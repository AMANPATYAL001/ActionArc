
namespace ActionArc.Application.Common.Results
{
	public class Error
	{
		public Error(string message, string code)
		{
			Message = message;
			Code = code;
		}

		public string Message { get; }
		public string Code { get; }
	}

	public class ErroCodes
	{
		public static string Todo_Title_Empty = "Todo_Title_Empty";
		public static string Invalid_DueDate = "Invalid_DueDate";
		public static string Todo_NotFound = "Todo_NotFound";
		public static string Invalid_PageNumber = "Invalid_PageNumber";
		public static string Invalid_PageSize = "Invalid_PageSize";
	}
}
