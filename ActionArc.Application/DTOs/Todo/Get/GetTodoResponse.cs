using ActionArc.Domain.Enums;

namespace ActionArc.Application.DTOs.Task.Get
{
	public class GetTodoResponse
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public TodoStatus Status { get; set; }
		public DateTime DueDate { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
