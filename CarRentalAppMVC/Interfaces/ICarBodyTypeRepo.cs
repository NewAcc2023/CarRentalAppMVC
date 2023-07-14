using CarRentalAppMVC.Entities;

namespace CarRentalAppMVC.Interfaces
{
	public interface ICarBodyTypeRepo
	{
		public IQueryable<CarBodyType> GetAll();

		public Task AddCarBodyType(CarBodyType carBodyType);
		public Task UpdateCarBodyType(int id, string name);
		public Task DeleteCarBodyType(int id);
	}
}
