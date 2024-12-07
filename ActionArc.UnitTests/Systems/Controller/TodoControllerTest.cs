using ActionArc.Application.Common.Enums;
using ActionArc.Application.Common.Results;
using ActionArc.Application.DTOs.Todo.GetTodos;
using ActionArc.Application.Services.Task;
using ActionArc.Controllers;
using ActionArc.UnitTests.Fixtures;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ActionArc.UnitTests.Systems.Controller
{
	public class TodoControllerTest
	{
		[Fact]
		public async Task GetAllTodos_OnSuccess_ReturnStatusCode200()
		{
			// Arrange
			var todoService = new Mock<ITodoService>();
			todoService.Setup(s => s.GetTodos(TodoFixture.AllStatus, null, TodoFixture.ValidPageNumber, TodoFixture.ValidPageSize)).ReturnsAsync(new GetTodosResponse());

			var todoAcontroller = new TodoController(todoService.Object);

			// Act

			var result = (OkObjectResult)await todoAcontroller.GetTodos(TodoFixture.AllStatus, null, TodoFixture.ValidPageNumber, TodoFixture.ValidPageSize);

			// Assert

			result.StatusCode.Should().Be(200);
		}

		[Fact]
		public async Task GetAllTodos_OnSuccess_ReturnListOfTodo()
		{
			// Arrange
			var todoService = new Mock<ITodoService>();
			todoService.Setup(s => s.GetTodos(TodoFixture.AllStatus,
									 null,
									 TodoFixture.ValidPageNumber,
									 TodoFixture.ValidPageSize)).ReturnsAsync(new GetTodosResponse { Todos = new() });

			var todoAcontroller = new TodoController(todoService.Object);

			// Act

			var result = (OkObjectResult)await todoAcontroller.GetTodos(TodoFixture.AllStatus, null, TodoFixture.ValidPageNumber, TodoFixture.ValidPageSize);

			var responseObject = (GetTodosResponse)result.Value;

			// Assert

			result.Should().BeOfType<OkObjectResult>();

			result.Value.Should().BeOfType<GetTodosResponse>();

			responseObject.Todos.Should().BeOfType<List<TodoItem>>();
		}

		[Fact]
		public async Task GetAllTodos_WithInvalid_PageNumber()
		{
			// Arrange
			var todoService = new Mock<ITodoService>();
			todoService.Setup(s => s.GetTodos(TodoFixture.AllStatus,
									 null,
									 TodoFixture.InValidPageNumber,
									 TodoFixture.ValidPageSize))
						.ReturnsAsync(new Error("Page number should be greater than 0", ErroCodes.Invalid_PageNumber));

			var todoAcontroller = new TodoController(todoService.Object);

			// Act

			var result = (BadRequestObjectResult)await todoAcontroller.GetTodos(TodoFixture.AllStatus,
									 null,
									 TodoFixture.InValidPageNumber,
									 TodoFixture.ValidPageSize);

			// Assert

			result.Should().BeOfType<BadRequestObjectResult>();

			result.StatusCode.Should().Be(400);

			result.Value.Should().BeOfType<Error>();

			Assert.Equal(ErroCodes.Invalid_PageNumber, ((Error)result.Value).Code);
		}

		[Fact]
		public async Task GetAllTodos_WithInvalid_PageSize()
		{
			// Arrange
			var todoService = new Mock<ITodoService>();
			todoService.Setup(s => s.GetTodos(TodoFixture.AllStatus,
									 null,
									 TodoFixture.ValidPageNumber,
									 TodoFixture.InValidPageSize))
				.ReturnsAsync(new Error("Page Size should be greater than 0", ErroCodes.Invalid_PageSize));

			var todoAcontroller = new TodoController(todoService.Object);

			// Act

			var result = (BadRequestObjectResult)await todoAcontroller.GetTodos(TodoFixture.AllStatus, null, TodoFixture.ValidPageNumber, TodoFixture.InValidPageSize);

			// Assert

			result.Should().BeOfType<BadRequestObjectResult>();

			result.StatusCode.Should().Be(400);

			result.Value.Should().BeOfType<Error>();

			Assert.Equal(ErroCodes.Invalid_PageSize, ((Error)result.Value).Code);
		}

	}
}
