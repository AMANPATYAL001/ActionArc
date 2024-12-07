
using ActionArc.Domain.Entities;
using ActionArc.Domain.Enums;

namespace ActionArc.Infrastructure
{
	public class DatabaseSeeder
	{
		public static async Task Seed(ApplicationDbContext _dbContext)
		{
			if (!_dbContext.Todos.Any())
			{
				await _dbContext.Todos.AddRangeAsync(CreateTaskSeedData());

				await _dbContext.SaveChangesAsync();
			}
		}

		private static List<Todo> CreateTaskSeedData()
		{
			return new()
			{
				new()
				{
					Id=1,
					Title="Aman",
					Description="Patyal",
					Status=TodoStatus.Completed,
				}
			};
		}
	}
}
