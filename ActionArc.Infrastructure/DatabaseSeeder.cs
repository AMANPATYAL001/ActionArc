
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
					Title="Fix the Bug I Pretended Didn’t Exist",
					Description="I’ve ignored this issue for days. It’s time to actually face it and fix it... or maybe I’ll just try refreshing again",
					Status=TodoStatus.Pending,
					DueDate=DateTime.UtcNow.AddDays(2).AddHours(3)
				},
				new()
				{
					Id=2,
					Title="Read That StackOverflow Post That Should’ve Solved My Problem",
					Description="I’ve read the first 10 answers. Now I need to scroll down for the real solution... or just ask my coworker.",
					Status=TodoStatus.Pending,
					DueDate=DateTime.UtcNow.AddDays(4).AddHours(6)
				},
				new()
				{
					Id=3,
					Title="Refactor That Messy Code I Wrote Yesterday",
					Description="Yesterday’s code was written in a haze of coffee and deadlines. Time to clean it up... or just make it work with some comments.",
					Status=TodoStatus.Pending,
					DueDate=DateTime.UtcNow.AddDays(6).AddHours(23)
				},
				new()
				{
					Id=4,
					Title="Update NuGet Packages (Again)",
					Description="Every time I do this, something breaks. But if I don’t, my dependencies will revolt. Time for another update!",
					Status=TodoStatus.InProgress,
					DueDate=DateTime.UtcNow.AddHours(3)
				},
				new()
				{
					Id=5,
					Title="Fix Merge Conflict That’s Been Staring At Me",
					Description="I’ve avoided this merge conflict for two days. If I stare at it long enough, it might fix itself, right?",
					Status=TodoStatus.Completed,
					DueDate=DateTime.UtcNow.AddDays(-1).AddHours(3)
				},
				new()
				{
					Id=6,
					Title="Write Unit Tests (Because I Didn’t Write Them The First Time)",
					Description="I promised myself I’d write tests after the feature was done... it’s been 2 weeks, and I still haven’t done it.",
					Status=TodoStatus.Pending,
					DueDate=DateTime.UtcNow.AddDays(1).AddHours(13)
				}
			};
		}
	}
}
