
using ActionArc.Domain.Enums;

namespace ActionArc.Application.DTOs.Todo.GetTodos
{
	public class TodoItem
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public TodoStatus Status { get; set; }
		public DateTime DueDate { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? LastModifiedAt { get; set; }
	}
}
