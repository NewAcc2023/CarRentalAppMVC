using CarRentalAppMVC.Contexts;
using CarRentalAppMVC.Entities;
using CarRentalAppMVC.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Brand = CarRentalAppMVC.Entities.Brand;
using DriveType = CarRentalAppMVC.Entities.DriveType;

namespace CarRentalAppMVC.Repository
{
	public class DriveTypeRepo : IDriveTypeRepo
	{
		private readonly AppDbContext _context;

		public DriveTypeRepo(AppDbContext context)
		{
			_context = context;
		}

		public IQueryable<DriveType> GetAll()
		{
			return _context.DriveTypes.AsQueryable();
		}

		public async Task AddDriveType(DriveType driveType)
		{
			await _context.DriveTypes.AddAsync(driveType);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateDriveType(int id, string name)
		{
			DriveType driveType = await _context.DriveTypes.FirstOrDefaultAsync(x => x.Id == id);
			driveType.DriveTypeName = name;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteDriveType(int id)
		{
			DriveType driveType = await _context.DriveTypes.FirstOrDefaultAsync(x => x.Id == id);
			_context.DriveTypes.Remove(driveType);
			await _context.SaveChangesAsync();
		}
	}
}
