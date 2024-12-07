using ActionArc.Application.Common.Enums;

namespace ActionArc.UnitTests.Fixtures
{
	public class TodoFixture
	{
		public static TodoFilterStatus AllStatus = TodoFilterStatus.All;
		public static TodoFilterStatus PendingStatus = TodoFilterStatus.Pending;
		public static TodoFilterStatus CompletedStatus = TodoFilterStatus.Completed;
		public static TodoFilterStatus InProgressStatus = TodoFilterStatus.InProgress;

		public static int InValidPageNumber = -1;
		public static int InValidPageSize = -1;

		public static int ValidPageNumber = 1;
		public static int ValidPageSize = 1;
	}
}
