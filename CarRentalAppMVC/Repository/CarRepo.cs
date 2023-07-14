using CarRentalAppMVC.Contexts;
using CarRentalAppMVC.Entities;
using CarRentalAppMVC.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Text.RegularExpressions;

namespace CarRentalAppMVC.Repository
{
	public class CarRepo : ICarRepo
	{
		private readonly AppDbContext _context;

		public CarRepo(AppDbContext context)
		{
			_context = context;
		}
		//GET METHODS
		public IQueryable<Car> GetAll()
		{
			return _context.Cars
				.Include(x => x.CarBodyType)
				.Include(x => x.GearBox)
				.Include(x => x.EngineType)
				.Include(x => x.DriveType)
				.Include(x => x.Brand)
				.AsQueryable();
		}

		public Car GetCarById(int id)
		{
			return GetAll().FirstOrDefault(x => x.Id == id);
		}
		//CREATE
		public async Task AddCar(IFormFile image, Car car)
		{
			string uniqueFileName = DateTime.Now.Millisecond.ToString()
			+ Regex.Replace(image.FileName, "[^a-zA-Z0-9.]", "-").Replace(" ", "-");

			SaveCarImage(image, uniqueFileName);

			Car newCar = car;
			newCar.ImagePath = uniqueFileName;
			await _context.Cars.AddAsync(newCar);
			await _context.SaveChangesAsync();
		}
		//UPDATE
		public async Task UpdateCarPrice(int id, decimal price)
		{
			Car car = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);
			car.Price = price;
			await _context.SaveChangesAsync();
		}
		public async Task UpdateCar(IFormFile image, Car car)
		{
			Car carInDatabase = await _context.Cars.FirstOrDefaultAsync(x => x.Id == car.Id);

			if (image == null)
			{
				carInDatabase.BrandId = car.BrandId;
				carInDatabase.ModelName = car.ModelName;
				carInDatabase.GearBoxId = car.GearBoxId;
				carInDatabase.EngineTypeId = car.EngineTypeId;
				carInDatabase.EngineCapacity = car.EngineCapacity;
				carInDatabase.CarBodyTypeId = car.CarBodyTypeId;
				carInDatabase.Doors = car.Doors;
				carInDatabase.Seats = car.Seats;
				carInDatabase.Price = car.Price;
				carInDatabase.Year = car.Year;
				carInDatabase.Description = car.Description;
				carInDatabase.DriveTypeId = car.DriveTypeId;

				_context.Cars.Update(carInDatabase);
				_context.SaveChanges();
			}
			else
			{
				carInDatabase.BrandId = car.BrandId;
				carInDatabase.ModelName = car.ModelName;
				carInDatabase.GearBoxId = car.GearBoxId;
				carInDatabase.EngineTypeId = car.EngineTypeId;
				carInDatabase.EngineCapacity = car.EngineCapacity;
				carInDatabase.CarBodyTypeId = car.CarBodyTypeId;
				carInDatabase.Doors = car.Doors;
				carInDatabase.Seats = car.Seats;
				carInDatabase.Price = car.Price;
				carInDatabase.Year = car.Year;
				carInDatabase.Description = car.Description;
				carInDatabase.DriveTypeId = car.DriveTypeId;

				string uniqueFileName = DateTime.Now.Millisecond.ToString()
				+ Regex.Replace(image.FileName, "[^a-zA-Z0-9.]", "-").Replace(" ", "-");

				await DeleteCarImage(carInDatabase.ImagePath);
				await SaveCarImage(image, uniqueFileName);

				carInDatabase.ImagePath = uniqueFileName;

				_context.Cars.Update(carInDatabase);
				_context.SaveChanges();
			}
		}

		//DELETE
		public async Task DeleteCar(int carId)
		{
			Car carToDelete = await _context.Cars.FirstOrDefaultAsync(x => x.Id == carId);
			DeleteCarImage(carToDelete.ImagePath);
			var result = _context.Cars.Remove(carToDelete);
			await _context.SaveChangesAsync();
		}
		//SAVE AND DELETE IMAGES FROM FOLDER PRIVATE METHODS
		async Task SaveCarImage(IFormFile image, string imageName)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", imageName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await image.CopyToAsync(stream);
			}
		}

		async Task DeleteCarImage(string imageName)
		{
			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", imageName);

			using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.Delete))
			{
				File.Delete(filePath);
			}
		}


	}
}
