
using ActionArc.Domain.Enums;

namespace ActionArc.Application.DTOs.Todo.Update
{
	public class UpdateTodoRequest
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public TodoStatus Status { get; set; }
		public DateTime DueDate { get; set; }
	}
}
