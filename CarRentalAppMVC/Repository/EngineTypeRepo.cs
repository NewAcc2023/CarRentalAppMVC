using CarRentalAppMVC.Contexts;
using CarRentalAppMVC.Entities;
using CarRentalAppMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAppMVC.Repository
{
	public class EngineTypeRepo : IEngineTypeRepo
	{
		private readonly AppDbContext _context;
		public EngineTypeRepo(AppDbContext context)
		{
			_context = context;
		}

		public IQueryable<EngineType> GetAll()
		{
			return _context.EngineTypes.AsQueryable();
		}

		public async Task AddEngineType(EngineType engineType)
		{
			await _context.EngineTypes.AddAsync(engineType);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateEngineType(int id, string name)
		{
			EngineType engineType = await _context.EngineTypes.FirstOrDefaultAsync(x => x.Id == id);
			engineType.EngineTypeName = name;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteEngineType(int id)
		{
			EngineType engineType = await _context.EngineTypes.FirstOrDefaultAsync(x => x.Id == id);
			_context.EngineTypes.Remove(engineType);
			await _context.SaveChangesAsync();
		}
	}
}
