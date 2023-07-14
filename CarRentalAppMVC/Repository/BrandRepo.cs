using CarRentalAppMVC.Contexts;
using CarRentalAppMVC.Entities;
using CarRentalAppMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAppMVC.Repository
{
	public class BrandRepo : IBrandRepo
	{
		private readonly AppDbContext _context;

		public BrandRepo(AppDbContext context)
		{
			_context = context;
		}

		public IQueryable<Brand> GetAll()
		{
			return _context.Brands.AsQueryable();
		}

		public async Task AddBrand(Brand brand)
		{
			await _context.Brands.AddAsync(brand);
			await _context.SaveChangesAsync();
		}

        public async Task UpdateBrand(int id, string name)
		{
			Brand brand = await _context.Brands.FirstOrDefaultAsync(x=> x.Id == id);
			brand.BrandName = name;
			await _context.SaveChangesAsync();
		}

        public async Task DeleteBrand(int id)
		{
			Brand brandToDelete =  await _context.Brands.FirstOrDefaultAsync(x=>x.Id == id);
			_context.Brands.Remove(brandToDelete);
			await _context.SaveChangesAsync();
		}
	}
}
