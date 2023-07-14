using CarRentalAppMVC.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using DriveType = CarRentalAppMVC.Entities.DriveType;

namespace CarRentalAppMVC.Contexts
{
	public class AppDbContext : IdentityDbContext
	{
        private readonly DbContextOptions _options;

		public DbSet<Car> Cars { get; set; }
		public DbSet<Brand> Brands { get; set; }

		public DbSet<CarBodyType> CarBodyTypes { get; set; }

		public DbSet<Entities.DriveType> DriveTypes { get; set; }

		public DbSet<Entities.EngineType> EngineTypes { get; set; }

		public DbSet<Entities.GearBox> GearBoxes { get; set; }

		public DbSet<Entities.RentOrder> RentOrders { get; set; }
		public DbSet<Entities.Status> Statuses { get; set; }
		public AppDbContext(DbContextOptions options) : base(options)
		{
			_options = options;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
