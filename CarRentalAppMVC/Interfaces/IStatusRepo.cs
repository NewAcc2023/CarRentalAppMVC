using CarRentalAppMVC.Entities;

namespace CarRentalAppMVC.Interfaces
{
	public interface IStatusRepo
	{
		public IQueryable<Status> GetAll();
	}
}
