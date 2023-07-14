using CarRentalAppMVC.Contexts;
using CarRentalAppMVC.Entities;
using Microsoft.AspNetCore.Identity;
using System.Net.Http.Headers;

namespace CarRentalAppMVC.Repository
{
	public class UserRepo
	{
		private readonly AppDbContext _context;

		public UserRepo(AppDbContext context)
		{
			_context = context;
		}

		public List<IdentityUser> GetAll()
		{
			return _context.Users.ToList();
		}
	}
}
