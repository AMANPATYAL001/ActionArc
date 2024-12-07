using ActionArc.Application.Services.Task;
using Microsoft.Extensions.DependencyInjection;

namespace ActionArc.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddScoped<ITodoService, TodoService>();

			return services;
		}

	}
}
