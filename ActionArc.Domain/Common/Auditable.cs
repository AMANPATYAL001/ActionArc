namespace ActionArc.Domain.Common
{
	public interface IAuditable
	{
		public DateTime CreatedAt { get; set; }
		public DateTime? LastModifiedAt { get; set; }
		public DateTime? DeletedAt { get; set; }
		public bool IsDeleted { get; set; }
	}
}
