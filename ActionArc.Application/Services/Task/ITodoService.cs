using ActionArc.Application.Common.Enums;
using ActionArc.Application.Common.Results;
using ActionArc.Application.DTOs.Task.Get;
using ActionArc.Application.DTOs.Todo.Create;
using ActionArc.Application.DTOs.Todo.Delete;
using ActionArc.Application.DTOs.Todo.GetTodos;
using ActionArc.Application.DTOs.Todo.Update;

namespace ActionArc.Application.Services.Task
{
	public interface ITodoService
	{
		Task<Result<GetTodoResponse>> GetTodoById(int id);

		Task<Result<GetTodosResponse>> GetTodos(TodoFilterStatus status, DateTime? dueDate, int pageNo, int pageSize);

		Task<Result<CreateTodoResponse>> CreateTodo(CreateTodoRequest createTodo);

		Task<Result<DeleteTodoResponse>> DeleteTodo(int id);

		Task<Result<UpdateTodoResponse>> UpdateTodo(int id, UpdateTodoRequest updateTodo);

		Task<Result<string>> DownloadCSV();
	}
}
