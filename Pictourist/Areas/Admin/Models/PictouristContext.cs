using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PictouristAPI.Areas.Admin.Models
{
	public class PictouristContext : IdentityDbContext<User>
	{
		public DbSet<Picture> Pictures { get; set; }

		public PictouristContext(DbContextOptions<PictouristContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.HasMany(e => e.Friends)
				.WithMany();

			modelBuilder.Entity<User>()
				.HasMany(e => e.Pictures)
				.WithMany();
		}

		//protected override void OnModelCreating(ModelBuilder builder)
		//{
		//    builder.Entity<User>()
		//    .Metadata.FindNavigation(nameof(User.UserPhotos))
		//    .SetPropertyAccessMode(PropertyAccessMode.Field);
		//}
	}
}
