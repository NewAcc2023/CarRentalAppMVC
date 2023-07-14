using Microsoft.AspNetCore.Identity;

namespace CarRentalAppMVC.Entities
{
	public class AppUser : IdentityUser
	{
		public virtual ICollection<RentOrder> RentOrders { get; set; }
	}
}
