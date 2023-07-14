using CarRentalAppMVC.Contexts;
using CarRentalAppMVC.Entities;
using CarRentalAppMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAppMVC.Repository
{
	public class GearBoxRepo : IGearBoxRepo
	{
		private readonly AppDbContext _context;

		public GearBoxRepo(AppDbContext context)
		{
			_context = context;
		}

		public IQueryable<GearBox> GetAll()
		{
			return _context.GearBoxes.AsQueryable();
		}

		public async Task AddGearBox(GearBox gearBox)
		{
			await _context.GearBoxes.AddAsync(gearBox);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateGearBox(int id, string name)
		{
			GearBox gearBox = await _context.GearBoxes.FirstOrDefaultAsync(x => x.Id == id);
			gearBox.GearBoxName = name;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteGearBox(int id)
		{
			GearBox gearBox = await _context.GearBoxes.FirstOrDefaultAsync(x => x.Id == id);
			_context.GearBoxes.Remove(gearBox);
			await _context.SaveChangesAsync();
		}
	}
}
