using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> _logger) : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		_logger.LogError(exception.Message);

		var details = new ProblemDetails
		{
			Detail = "",
			Instance = "API",
			Title = "",
			Type = "",
			Status = httpContext.Response.StatusCode,
		};

		var response = JsonSerializer.Serialize(details);

		httpContext.Response.ContentType = "application/json";

		await httpContext.Response.WriteAsync(response, cancellationToken);

		return true;
	}
}