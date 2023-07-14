using CarRentalAppMVC.Entities;
using DriveType = CarRentalAppMVC.Entities.DriveType;

namespace CarRentalAppMVC.Models
{
	public class FullEditModel
	{
		public Car Car { get; set; }
		public List<Brand> Brands { get; set; }
		public List<GearBox> GearBoxes { get; set; }
		public List<EngineType> EngineTypes { get; set; }
		public List<CarBodyType> CarBodyTypes { get; set; }
		public List<DriveType> DriveTypes { get; set; }


	}
}
