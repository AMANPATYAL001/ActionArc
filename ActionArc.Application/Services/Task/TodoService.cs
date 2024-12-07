using ActionArc.Application.Common.Enums;
using ActionArc.Application.Common.Results;
using ActionArc.Application.DTOs.Task.Get;
using ActionArc.Application.DTOs.Todo.Create;
using ActionArc.Application.DTOs.Todo.Delete;
using ActionArc.Application.DTOs.Todo.GetTodos;
using ActionArc.Application.DTOs.Todo.Update;
using ActionArc.Domain.Entities;
using ActionArc.Domain.Enums;
using ActionArc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;

namespace ActionArc.Application.Services.Task
{
	public class TodoService(ApplicationDbContext _dbContext, ILogger<TodoService> _logger) : ITodoService
	{
		public async Task<Result<CreateTodoResponse>> CreateTodo(CreateTodoRequest createTodo)
		{
			if (string.IsNullOrEmpty(createTodo.Title))
				return new Error("Title is Empty", ErroCodes.Todo_Title_Empty);

			if (createTodo.DueDate > DateTime.UtcNow)
				return new Error("Due Date Should be of Future", ErroCodes.Invalid_DueDate);

			var todo = new Todo
			{
				Title = createTodo.Title,
				Description = createTodo.Description,
				DueDate = createTodo.DueDate,
				Status = TodoStatus.Pending,
			};

			await _dbContext.AddAsync(todo);

			await _dbContext.SaveChangesAsync();

			var createdTodoResponse = new CreateTodoResponse
			{
				Id = todo.Id,
				Title = createTodo.Title,
				CreatedOn = todo.CreatedAt
			};

			_logger.LogInformation("Todo {TodoId} created", todo.Id);

			return createdTodoResponse;
		}

		public async Task<Result<DeleteTodoResponse>> DeleteTodo(int id)
		{
			var todo = await _dbContext.Todos.FirstOrDefaultAsync(x => x.Id == id);
			if (todo is null)
				return new Error("Invalid Todo Id", ErroCodes.Todo_NotFound);

			todo!.IsDeleted = true;
			_dbContext.Todos.Remove(todo);

			await _dbContext.SaveChangesAsync();

			var response = new DeleteTodoResponse { Message = $"Task '{todo.Title}' has been successfully deleted" };

			_logger.LogInformation("Todo {TodoId} soft deleted", todo.Id);

			return response;
		}

		public async Task<Result<GetTodoResponse>> GetTodoById(int id)
		{
			var todo = await _dbContext.Todos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

			if (todo == null)
				return Result<GetTodoResponse>.Failure(new Error("Todo Doesn't exist", ErroCodes.Todo_NotFound));

			var todoResponse = new GetTodoResponse
			{
				Title = todo.Title,
				Description = todo.Description,
				Status = todo.Status,
				DueDate = todo.DueDate,
				CreatedAt = todo.CreatedAt,
			};

			return Result<GetTodoResponse>.Success(todoResponse);
		}

		public async Task<Result<GetTodosResponse>> GetTodos(TodoFilterStatus status, DateTime? dueDate, int pageNo, int pageSize)
		{
			if (pageNo < 1)
				return new Error("Page number should be greater than 0", ErroCodes.Invalid_PageNumber);

			if (pageSize < 1)
				return new Error("Page Size should be greater than 0", ErroCodes.Invalid_PageSize);

			var validStatus = GetTodoStatusFilter(status);

			var todos = _dbContext.Todos.AsNoTracking().Where(x =>
												(!validStatus.HasValue || x.Status == validStatus) &&
												(!dueDate.HasValue || x.DueDate <= dueDate)
												).Skip(pageNo - 1 * pageSize).Take(pageSize);
			var todosResponse = new GetTodosResponse
			{
				Todos = todos.Select(x => new TodoItem
				{
					Id = x.Id,
					Title = x.Title,
					Description = x.Description,
					Status = x.Status,
					DueDate = x.DueDate,
					CreatedAt = x.CreatedAt,
					LastModifiedAt = x.LastModifiedAt
				}).ToList(),
			};
			var fetchTodoCount = todosResponse.Todos.Count;
			_logger.LogInformation("Total {TodoCount} Todo fetched", fetchTodoCount);

			return todosResponse;
		}

		public async Task<Result<UpdateTodoResponse>> UpdateTodo(int id, UpdateTodoRequest updateTodo)
		{
			if (string.IsNullOrEmpty(updateTodo.Title))
				return new Error("Title is Empty", ErroCodes.Todo_Title_Empty);

			if (updateTodo.DueDate < DateTime.UtcNow)
				return new Error("Due Date Should be of Future", ErroCodes.Invalid_DueDate);

			var todo = await _dbContext.Todos.FirstOrDefaultAsync(x => x.Id == id);
			if (todo is null)
				return new Error("Todo Doesn't exist", ErroCodes.Todo_NotFound);

			todo.Title = updateTodo.Title;
			todo.Description = updateTodo.Description;
			todo.Status = updateTodo.Status;
			todo.DueDate = updateTodo.DueDate;

			_dbContext.Todos.Update(todo);

			await _dbContext.SaveChangesAsync();

			var response = new UpdateTodoResponse { Message = $"'{todo.Title}' update successfully." };

			_logger.LogInformation("Todo {TodoId} updated", todo.Id);

			return response;
		}

		public async Task<Result<string>> DownloadCSV()
		{
			var todos = await _dbContext.Todos.IgnoreQueryFilters().ToListAsync();

			var csvString = GenerateCSVData(todos);

			return csvString;
		}

		private TodoStatus? GetTodoStatusFilter(TodoFilterStatus status) => TodoFilterStatus.InProgress == status ? TodoStatus.InProgress : (TodoFilterStatus.Pending == status ? TodoStatus.Pending : (TodoFilterStatus.Completed == status ? TodoStatus.Completed : null));

		private string GenerateCSVData(IEnumerable<Todo> todos)
		{
			var csvDataString = new StringBuilder();

			csvDataString.Append("Title,Description,Status,DueDate,CreatedAt");

			foreach (var todo in todos)
			{
				csvDataString.Append($"\n{todo.Title},{todo.Description},{todo.Status.ToString()},{todo.DueDate.ToString("MM/dd/yyyy hh:mm tt")},{todo.CreatedAt.ToString("MM/dd/yyyy hh:mm tt")}");
			}

			return csvDataString.ToString();
		}
	}
}
