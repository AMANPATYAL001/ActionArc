using ActionArc.Application.Common.Enums;
using ActionArc.Application.DTOs.Todo.Create;
using ActionArc.Application.DTOs.Todo.Update;
using ActionArc.Application.Services.Task;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ActionArc.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class TodoController(ITodoService _todoService/*, ILogger<TodoController> _logger*/) : ControllerBase
	{

		[HttpGet(Name = "Get")]
		[EndpointSummary("Get a Todo 🔍")]
		[EndpointDescription("Lost something? Find your task by its ID, like a treasure map for procrastinators.")]
		public async Task<IActionResult> Get([FromQuery] int id)
		{
			var result = await _todoService.GetTodoById(id);

			return result.Map<IActionResult>(
				onSuccess: Ok,
				onFailure: BadRequest
			);
		}

		[HttpGet(Name = "GetTodos")]
		[EndpointSummary("Get Todos 🗃️")]
		[EndpointDescription("Discover the chaos you’ve created—retrieve all your tasks in one go.")]
		public async Task<IActionResult> GetTodos(TodoFilterStatus status, DateTime? dueDate, int pageNo = 1, int pageSize = 5)
		{
			var result = await _todoService.GetTodos(status, dueDate, pageNo, pageSize);

			return result.Map<IActionResult>(
				onSuccess: Ok,
				onFailure: BadRequest
			);
		}

		[HttpPost(Name = "Add Todo")]
		[EndpointSummary("Create a Todo ✏️")]
		[EndpointDescription("Bring your dreams to life! Add a new task to your never-ending list.")]
		public async Task<IActionResult> Create(CreateTodoRequest createTodo)
		{
			var result = await _todoService.CreateTodo(createTodo);

			return result.Map<IActionResult>(
				onSuccess: x => Created(Url.Action(nameof(Get), new { id = x.Id }), x),
				onFailure: BadRequest
			);
		}

		[HttpPut(Name = "Update Todo")]
		[EndpointSummary("Update a Todo ✒️")]
		[EndpointDescription("Give your task a makeover—update it with a fresh new vibe.")]
		public async Task<IActionResult> Update(int id, UpdateTodoRequest updateTodo)
		{
			var result = await _todoService.UpdateTodo(id, updateTodo);

			return result.Map<IActionResult>(
				onSuccess: Ok,
				onFailure: BadRequest
			);
		}

		[HttpDelete(Name = "Delete Todo")]
		[EndpointSummary("Delete a Todo 🗑️")]
		[EndpointDescription("Say goodbye to old priorities—delete a task and lighten your load!")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _todoService.DeleteTodo(id);

			return result.Map<IActionResult>(
				onSuccess: x => NoContent(),
				onFailure: BadRequest
			);
		}

		[HttpGet(Name = "Download CSV")]
		[EndpointSummary("Download Todo CSV 🖇️")]
		[EndpointDescription("Get your tasks in a CSV format - because who doesn't love spreadsheets?")]
		public async Task<IActionResult> DownloadCSV()
		{
			var result = await _todoService.DownloadCSV();

			return result.Map<IActionResult>(
				onSuccess: Ok,
				onFailure: BadRequest
			);
		}

	}
}
