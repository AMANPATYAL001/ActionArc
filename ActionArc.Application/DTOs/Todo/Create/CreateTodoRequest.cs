﻿
namespace ActionArc.Application.DTOs.Todo.Create
{
	public class CreateTodoRequest
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime DueDate { get; set; }
	}
}
