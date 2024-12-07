using ActionArc.Domain.Common;
using ActionArc.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ActionArc.Domain.Entities
{
	public class Todo : IAuditable
	{
		public int Id { get; set; }
		[Required]
		public string Title { get; set; }
		public string Description { get; set; }
		public TodoStatus Status { get; set; }
		public DateTime DueDate { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime? LastModifiedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
		public bool IsDeleted { get; set; }
	}
}
