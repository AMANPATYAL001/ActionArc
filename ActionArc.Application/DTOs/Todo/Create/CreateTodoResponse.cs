
namespace ActionArc.Application.DTOs.Todo.Create
{
	public class CreateTodoResponse
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime CreatedOn { get; set; }
		public string Message = "Task Created Successfully";
	}
}
