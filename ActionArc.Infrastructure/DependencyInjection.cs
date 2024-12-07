using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ActionArc.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseInMemoryDatabase(services.BuildServiceProvider().GetRequiredService<IConfiguration>().GetConnectionString("InMemoryDB") ?? throw new Exception())
			);
			return services;
		}
	}
}
