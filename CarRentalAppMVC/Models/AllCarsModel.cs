using CarRentalAppMVC.Entities;
using System.ComponentModel.DataAnnotations;
using DriveType = CarRentalAppMVC.Entities.DriveType;

namespace CarRentalAppMVC.Models
{
	public class AllCarsModel
	{
		public List<Car> Cars { get; set; }
		public List<Brand> Brands { get; set; }
		public List<GearBox> GearBoxes { get; set; }
		public List<EngineType> EngineTypes { get; set; }
		public List<CarBodyType> CarBodyTypes { get; set; }
		public List<DriveType> DriveTypes { get; set; }


	}
}
