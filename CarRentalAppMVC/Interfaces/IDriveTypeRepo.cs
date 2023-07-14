using CarRentalAppMVC.Entities;
using DriveType = CarRentalAppMVC.Entities.DriveType;

namespace CarRentalAppMVC.Interfaces
{
	public interface IDriveTypeRepo
	{
		public IQueryable<DriveType> GetAll();

		public Task AddDriveType(DriveType driveType);
		public Task UpdateDriveType(int id, string name);
		public Task DeleteDriveType(int id);
	}
}
