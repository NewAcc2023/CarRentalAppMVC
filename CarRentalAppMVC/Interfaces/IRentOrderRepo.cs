using CarRentalAppMVC.Entities;
using CarRentalAppMVC.Models;

namespace CarRentalAppMVC.Interfaces
{
	public interface IRentOrderRepo
	{
		public IQueryable<RentOrder> GetAll();
		public Task CreateOrder(RentOrder rentOrder);
		public RentOrdersPaginationModel GetPaginatedRentOrders(int pageNumber, int pageSize);
		public Task Finish(int id);
		public Task Cancel(int id);
		public Task SetActive(int id);
	}
}
