using CarRentalAppMVC.Contexts;
using CarRentalAppMVC.Entities;
using CarRentalAppMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAppMVC.Repository
{
	public class StatusRepo : IStatusRepo
	{
		private readonly AppDbContext _context;
		public StatusRepo(AppDbContext context)
		{
			_context = context;
		}

		public IQueryable<Status> GetAll()
		{
			return _context.Statuses.AsQueryable();
		}
	}
}
