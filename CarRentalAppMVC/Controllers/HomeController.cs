
using CarRentalAppMVC.Entities;
using CarRentalAppMVC.Interfaces;
using CarRentalAppMVC.Models;
using Microsoft.AspNetCore.Mvc;

using System.Text.Json.Serialization;
using System.Text.Json;

namespace CarRentalAppMVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ICarRepo _carRepo;
		private readonly IBrandRepo _brandRepo;
		private readonly ICarBodyTypeRepo _carBodyTypeRepo;
		private readonly IDriveTypeRepo _driveTypeRepo;
		private readonly IEngineTypeRepo _engineTypeRepo;
		private readonly IGearBoxRepo _gearBoxRepo;

		public HomeController(ILogger<HomeController> logger, ICarRepo carRepo, IBrandRepo brandRepo, ICarBodyTypeRepo carBodyTypeRepo, IDriveTypeRepo driveTypeRepo, IEngineTypeRepo engineTypeRepo, IGearBoxRepo gearBoxRepo)
		{
			_logger = logger;
			_carRepo = carRepo;
			_brandRepo = brandRepo;
			_carBodyTypeRepo = carBodyTypeRepo;
			_driveTypeRepo = driveTypeRepo;
			_engineTypeRepo = engineTypeRepo;
			_gearBoxRepo = gearBoxRepo;
		}

		public async Task<IActionResult> Index()
		{
			List<Car> cars = _carRepo.GetAll().ToList();

			List<IndexModel> indexModels = new List<IndexModel>();

			foreach (var car in cars)
			{
				indexModels.Add(new IndexModel
				{
					Id = car.Id,
					Brand = car.Brand.BrandName,
					ImagePath = car.ImagePath,
					Model = car.ModelName,
					StartPrice = car.Price,
				});
			}
			return View(indexModels);
		}

		public async Task<IActionResult> SearchCars(string search)
		{
			if(search != null && search.Count() > 0)
			{
				JsonSerializerOptions options = new JsonSerializerOptions
				{
					ReferenceHandler = ReferenceHandler.Preserve,
					MaxDepth = 32 // Set the maximum allowed depth as needed
				};

				List<Car> cars = _carRepo.GetAll().Where(x => x.Brand.BrandName.ToLower().Contains(search.ToLower()) ||
													x.ModelName.ToLower().Contains(search.ToLower())).ToList();

				List<SearchResult> searchResults = new List<SearchResult>();

				foreach (var car in cars)
				{
					SearchResult searchResult = new SearchResult();
					searchResult.Id = car.Id;
					searchResult.BrandName = car.Brand.BrandName;
					searchResult.ModelName = car.ModelName;
					searchResult.Price = car.Price;
					searchResults.Add(searchResult);
				}
				return Json(searchResults, options);
			}
			return RedirectToAction("Index");
		}

		public async Task<IActionResult> AllCars( List<string> selectedGearBoxes, List<string> selectedEngineTypes, List<string> selectedBrands, string? sorting = "")
		{
			ViewBag.selectedEngineTypes = selectedEngineTypes;
			ViewBag.selectedGearBoxes = selectedGearBoxes;
			ViewBag.selectedBrands =selectedBrands;
			ViewBag.sorting = sorting;
		   TempData["Sorted"] = sorting;
			List<Car> cars = _carRepo.GetAll().ToList();
			//filter
			if (selectedGearBoxes.Count > 0)
				cars = cars.Where(x => selectedGearBoxes.Contains(x.GearBox.GearBoxName)).ToList();
			if (selectedEngineTypes.Count > 0)
				cars = cars.Where(x => selectedEngineTypes.Contains(x.EngineType.EngineTypeName)).ToList();
			if (selectedBrands.Count > 0)
				cars = cars.Where(x => selectedBrands.Contains(x.Brand.BrandName)).ToList();

			AllCarsModel allCarsModel = new AllCarsModel()
			{
				Cars = cars,
				Brands = _brandRepo.GetAll().ToList(),
				GearBoxes = _gearBoxRepo.GetAll().ToList(),
				EngineTypes = _engineTypeRepo.GetAll().ToList()
			};

			//sort
			if (sorting == "cheapest")
			{
				AllCarsModel allCarsModelSorted = allCarsModel;
				allCarsModelSorted.Cars = allCarsModel.Cars.OrderBy(x => x.Price).ToList();
				return View(allCarsModelSorted);
			}
			else if (sorting == "expensive")
			{
				AllCarsModel allCarsModelSorted = allCarsModel;
				allCarsModelSorted.Cars = allCarsModel.Cars.OrderByDescending(x => x.Price).ToList();
				return View(allCarsModelSorted);
			}
			else if (sorting == "newest")
			{
				AllCarsModel allCarsModelSorted = allCarsModel;
				allCarsModelSorted.Cars = allCarsModel.Cars.OrderByDescending(x => x.Year).ToList();
				return View(allCarsModelSorted);
			}
			//if anything else
			return View(allCarsModel);
		}

		public async Task<IActionResult> SomethingWentWrong()
		{
			return View();
		}
	}
}