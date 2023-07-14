using CarRentalAppMVC.Entities;
using Brand = CarRentalAppMVC.Entities.Brand;
using DriveType = CarRentalAppMVC.Entities.DriveType;

namespace CarRentalAppMVC.Models
{
    public class AddCarModel
    {
        public List<Brand> Brands { get; set; }

        public string ModelName { get; set; }

        public List<GearBox> GearBoxes { get; set; }

        public List<EngineType> EngineTypes { get; set; }

        public string EngineCapacity { get; set; }

        public List<CarBodyType> CarBodyTypes { get; set; }

        public int Doors { get; set; }

        public int Seats { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }

        public List<DriveType> DriveTypes { get; set; }

        public string ImagePath { get; set; }
    }
}
