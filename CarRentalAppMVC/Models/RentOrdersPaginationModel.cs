using CarRentalAppMVC.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarRentalAppMVC.Models
{
	public class RentOrdersPaginationModel
	{
		public IEnumerable<RentOrder> RentOrders { get; set; }
		public IEnumerable<IdentityUser> Users { get; set; }
		public IEnumerable<Car> Cars { get; set; }
		public IEnumerable<Status> Statuses { get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
	}
}
