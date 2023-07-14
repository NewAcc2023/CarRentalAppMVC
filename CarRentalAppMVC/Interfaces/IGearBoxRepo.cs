using CarRentalAppMVC.Entities;

namespace CarRentalAppMVC.Interfaces
{
	public interface IGearBoxRepo
	{
		public IQueryable<GearBox> GetAll();
		public Task AddGearBox(GearBox brand);
		public Task UpdateGearBox(int id, string name);
		public Task DeleteGearBox(int id);
	}
}
