using ActionArc.Domain.Common;
using ActionArc.Domain.Entities;
using ActionArc.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace ActionArc.Infrastructure
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
	{
		public DbSet<Todo> Todos { get; set; }

		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//=> optionsBuilder.AddInterceptors(new AuditInterceptor());
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Todo>().HasQueryFilter(x => !x.IsDeleted);
		}
		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			var entities = ChangeTracker.Entries<IAuditable>();
			DateTime utcNow = DateTime.UtcNow;

			foreach (EntityEntry<IAuditable> entry in entities)
			{
				if (entry.State == EntityState.Added)
					entry.Entity.CreatedAt = utcNow;


				if (entry.State == EntityState.Modified)
					entry.Entity.LastModifiedAt = utcNow;


				if (entry.State == EntityState.Deleted)
				{
					entry.State = EntityState.Modified;
					entry.Entity.DeletedAt = utcNow;
					entry.Entity.IsDeleted = true;
				}
			}

			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
	}
}