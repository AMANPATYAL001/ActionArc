
using ActionArc.Application;
using ActionArc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

namespace ActionArc
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddApplication().AddInfrastructure();

			builder.Services.AddExceptionHandler<ExceptionHandlingMiddleware>();
			builder.Services.AddProblemDetails();

			builder.Services.AddControllers();
			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			builder.Services.AddOpenApi();
			//builder.Services.AddSingleton<AuditInterceptor>();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			await SeedDatabase(app);

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
				app.MapScalarApiReference(opt =>
				{
					opt
					.AddServer(new ScalarServer(Url: "https://t6105b8p-7108.inc1.devtunnels.ms/", "For Deployment"))
					.AddServer(new ScalarServer(Url: "https://localhost:7108/", "For Local"))
					.WithTitle("Aman")
					.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
					.WithModels(false)
					.WithTheme(ScalarTheme.BluePlanet);
				});
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.UseExceptionHandler();

			app.MapControllers();

			app.Run();
		}

		private static async Task SeedDatabase(WebApplication application)
		{
			using (var scope = application.Services.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
				await DatabaseSeeder.Seed(context);
			}
		}
	}

}
