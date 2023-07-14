using CarRentalAppMVC.Contexts;
using CarRentalAppMVC.Entities;
using CarRentalAppMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAppMVC.Repository
{
	public class CarBodyTypeRepo : ICarBodyTypeRepo
	{
		private readonly AppDbContext _context;
		public CarBodyTypeRepo(AppDbContext context)
		{
			_context = context;
		}

		public IQueryable<CarBodyType> GetAll()
		{
			return _context.CarBodyTypes.AsQueryable();
		}

		public async Task AddCarBodyType(CarBodyType carBodyType)
		{
			await _context.CarBodyTypes.AddAsync(carBodyType);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateCarBodyType(int id, string name)
		{
			CarBodyType carBodyType = await _context.CarBodyTypes.FirstOrDefaultAsync(x => x.Id == id);
			carBodyType.CarBodyTypeName = name;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteCarBodyType(int id)
		{
			CarBodyType carBodyType = await _context.CarBodyTypes.FirstOrDefaultAsync(x => x.Id == id);
			_context.CarBodyTypes.Remove(carBodyType);
			await _context.SaveChangesAsync();
		}
	}
}
