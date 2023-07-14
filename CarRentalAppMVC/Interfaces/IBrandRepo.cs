using CarRentalAppMVC.Entities;
using Microsoft.Identity.Client;

namespace CarRentalAppMVC.Interfaces
{
	public interface IBrandRepo
	{
		public IQueryable<Brand> GetAll();
		public Task AddBrand(Brand brand);
		public Task UpdateBrand(int id, string name);
		public Task DeleteBrand(int id);
	}
}
