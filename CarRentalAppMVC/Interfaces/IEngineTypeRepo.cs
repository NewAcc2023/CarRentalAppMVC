using CarRentalAppMVC.Entities;

namespace CarRentalAppMVC.Interfaces
{
	public interface IEngineTypeRepo
	{
		public IQueryable<EngineType> GetAll();
		public Task AddEngineType(EngineType brand);
		public Task UpdateEngineType(int id, string name);
		public Task DeleteEngineType(int id);
	}
}
