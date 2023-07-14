using CarRentalAppMVC.Contexts;
using CarRentalAppMVC.Entities;
using CarRentalAppMVC.Interfaces;
using CarRentalAppMVC.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace CarRentalAppMVC.Repository
{
	public class RentOrderRepo : IRentOrderRepo
	{
		private readonly AppDbContext _context;

		public RentOrderRepo(AppDbContext context)
		{
			_context = context;
		}

		public async Task CreateOrder(RentOrder rentOrder)
		{
			await _context.RentOrders.AddAsync(rentOrder);
			await _context.SaveChangesAsync();
		}

		public IQueryable<RentOrder> GetAll()
		{
			return _context.RentOrders
				.Include(x => x.Car)
				.Include(x => x.Status)
				.AsQueryable();
		}

		public RentOrdersPaginationModel GetPaginatedRentOrders(int pageNumber, int pageSize)
		{
			// Fetch the total count of cars from the database
			int totalCount = _context.RentOrders.Count();

			// Calculate the total number of pages based on the page size and total count
			int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

			// Fetch the cars for the specified page number
			IEnumerable<RentOrder> rentOrders = _context.RentOrders
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToList();
			
			return new RentOrdersPaginationModel
			{
				RentOrders = rentOrders,
				CurrentPage = pageNumber,
				PageSize = pageSize,
				TotalPages = totalPages
			};
		}

		public async Task Finish(int id)
		{
			RentOrder rentOrder = await _context.RentOrders.FirstAsync(x => x.Id == id);
			rentOrder.Status = await _context.Statuses.FirstAsync(x => x.StatusName == "Finished");
			_context.RentOrders.Update(rentOrder);
			await _context.SaveChangesAsync();
		}

		public async Task Cancel(int id)
		{
			RentOrder rentOrder = await _context.RentOrders.FirstAsync(x => x.Id == id);
			rentOrder.Status = await _context.Statuses.FirstAsync(x => x.StatusName == "Cancelled");
			_context.RentOrders.Update(rentOrder);
			await _context.SaveChangesAsync();
		}

		public async Task SetActive(int id)
		{
			RentOrder rentOrder = await _context.RentOrders.FirstAsync(x => x.Id == id);
			rentOrder.Status = await _context.Statuses.FirstAsync(x => x.StatusName == "Reserved");
			_context.RentOrders.Update(rentOrder);
			await _context.SaveChangesAsync();
		}
	}
}
