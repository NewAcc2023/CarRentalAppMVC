using CarRentalAppMVC.Entities;

namespace CarRentalAppMVC.Interfaces
{
	public interface ICarRepo
	{
		public IQueryable<Car> GetAll();
		public Car GetCarById(int id);
		public Task AddCar(IFormFile image, Car car);
		public Task UpdateCarPrice(int id, decimal price);
		public Task UpdateCar(IFormFile image, Car car);
		public Task DeleteCar(int carId);
	}
}
