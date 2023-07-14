using CarRentalAppMVC.Contexts;
using CarRentalAppMVC.Entities;
using CarRentalAppMVC.Interfaces;
using CarRentalAppMVC.Models;
using CarRentalAppMVC.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DriveType = CarRentalAppMVC.Entities.DriveType;

namespace CarRentalAppMVC.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly ICarRepo _carRepo;
		private readonly IBrandRepo _brandRepo;
		private readonly ICarBodyTypeRepo _carBodyTypeRepo;
		private readonly IDriveTypeRepo _driveTypeRepo;
		private readonly IEngineTypeRepo _engineTypeRepo;
		private readonly IGearBoxRepo _gearBoxRepo;
		private readonly IRentOrderRepo _rentOrderRepo;
		private readonly UserRepo _userRepo;
		private readonly IStatusRepo _statusRepo;
		public AdminController(UserRepo userRepo, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IStatusRepo statusRepo, ICarRepo carRepo, IBrandRepo brandRepo, ICarBodyTypeRepo carBodyTypeRepo, IDriveTypeRepo driveTypeRepo, IEngineTypeRepo engineTypeRepo, IGearBoxRepo gearBoxRepo, IRentOrderRepo rentOrderRepo)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_carRepo = carRepo;
			_brandRepo = brandRepo;
			_carBodyTypeRepo = carBodyTypeRepo;
			_driveTypeRepo = driveTypeRepo;
			_engineTypeRepo = engineTypeRepo;
			_gearBoxRepo = gearBoxRepo;
			_rentOrderRepo = rentOrderRepo;
			_userRepo = userRepo;
			_statusRepo = statusRepo;
		}
		//ADMIN VIEWS 
		public async Task<IActionResult> EditRoom()
		{
			AllCarsModel allCarsModel = new AllCarsModel();
			allCarsModel.Cars = _carRepo.GetAll().ToList();
			allCarsModel.Brands = _brandRepo.GetAll().ToList();
			allCarsModel.EngineTypes = _engineTypeRepo.GetAll().ToList();
			allCarsModel.GearBoxes = _gearBoxRepo.GetAll().ToList();
			allCarsModel.CarBodyTypes = _carBodyTypeRepo.GetAll().ToList();
			allCarsModel.DriveTypes = _driveTypeRepo.GetAll().ToList();
			return View(allCarsModel);
		}

		public async Task<IActionResult> FullEdit(int carId)
		{
			Car car = _carRepo.GetCarById(carId);
			if (car == null)
			{
				return RedirectToAction("SomethingWentWrong", "Home");
			}

			FullEditModel fullEdit = new FullEditModel()
			{
				Car = car,
				Brands = _brandRepo.GetAll().ToList(),
				GearBoxes = _gearBoxRepo.GetAll().ToList(),
				EngineTypes = _engineTypeRepo.GetAll().ToList(),
				CarBodyTypes = _carBodyTypeRepo.GetAll().ToList(),
				DriveTypes = _driveTypeRepo.GetAll().ToList()
			};
			
			return View(fullEdit);
		}


		[HttpPost]
		public async Task<IActionResult> FullEdit(IFormFile image, Car car )
		{
			await _carRepo.UpdateCar(image, car);
			return RedirectToAction("EditRoom");
		}

		public async Task<IActionResult> EditBrands()
		{
			return View(_brandRepo.GetAll().ToList());
		}

		public async Task<IActionResult> ManageOrders(int page = 1, int pageSize = 3)
		{
			RentOrdersPaginationModel rentOrdersPaginationModel = _rentOrderRepo.GetPaginatedRentOrders(page, pageSize);
			rentOrdersPaginationModel.Users = _userManager.Users;
			rentOrdersPaginationModel.Cars = _carRepo.GetAll();
			rentOrdersPaginationModel.Statuses = _statusRepo.GetAll();
			return View(rentOrdersPaginationModel);
		}

		public async Task<IActionResult> AddCar(AddCarModel model)
		{
			AddCarModel addCarModel = new AddCarModel();
			addCarModel.Brands = await _brandRepo.GetAll().ToListAsync();
			addCarModel.GearBoxes = await _gearBoxRepo.GetAll().ToListAsync();
			addCarModel.EngineTypes = await _engineTypeRepo.GetAll().ToListAsync();
			addCarModel.CarBodyTypes = await _carBodyTypeRepo.GetAll().ToListAsync();
			addCarModel.DriveTypes = await _driveTypeRepo.GetAll().ToListAsync();

			return View(addCarModel);
		}

		public async Task<IActionResult> EditCarBodyTypes()
		{
			return View(_carBodyTypeRepo.GetAll().ToList());
		}

		public async Task<IActionResult> EditDriveTypes()
		{
			return View(_driveTypeRepo.GetAll().ToList());
		}

		public async Task<IActionResult> EditGearBoxes()
		{
			return View(_gearBoxRepo.GetAll().ToList());
		}

		public async Task<IActionResult> EditEngineTypes()
		{
			return View(_engineTypeRepo.GetAll().ToList());
		}
		//CRUD for CAR
		[HttpPost]
		public async Task<IActionResult> UpdateCarPrice(int id, decimal price)
		{
			await _carRepo.UpdateCarPrice(id, price);
			return RedirectToAction("EditRoom");
		}

		[HttpPost]
		public async Task<IActionResult> AddCar(IFormFile image, Car car)
		{
			await _carRepo.AddCar(image, car);
			return RedirectToAction("EditRoom");
		}

		public async Task<IActionResult> DeleteCar(int carId)
		{
			await _carRepo.DeleteCar(carId);
			return RedirectToAction("EditRoom");
		}

		// CRUD for BRANDS
		[HttpPost]
		public async Task<IActionResult> AddBrand(Brand brand)
		{
			await _brandRepo.AddBrand(brand);
			return RedirectToAction("EditBrands");
		}

		[HttpPost]
		public async Task<IActionResult> UpdateBrand(int id, string name)
		{
			await _brandRepo.UpdateBrand(id, name);
			return RedirectToAction("EditBrands");
		}

		public async Task<IActionResult> DeleteBrand(int id)
		{
			await _brandRepo.DeleteBrand(id);
			return RedirectToAction("EditBrands");
		}

		// CRUD for CarBodyType
		[HttpPost]
		public async Task<IActionResult> AddCarBodyType(CarBodyType carBodyType)
		{
			await _carBodyTypeRepo.AddCarBodyType(carBodyType);
			return RedirectToAction("EditCarBodyTypes");
		}

		[HttpPost]
		public async Task<IActionResult> UpdateCarBodyType(int id, string name)
		{
			await _carBodyTypeRepo.UpdateCarBodyType(id, name);
			return RedirectToAction("EditCarBodyTypes");
		}

		public async Task<IActionResult> DeleteCarBodyType(int id)
		{
			await _carBodyTypeRepo.DeleteCarBodyType(id);
			return RedirectToAction("EditCarBodyTypes");
		}

		// CRUD for DRIVETYPE
		[HttpPost]
		public async Task<IActionResult> AddDriveType(DriveType driveType)
		{
			await _driveTypeRepo.AddDriveType(driveType);
			return RedirectToAction("EditDriveTypes");
		}

		[HttpPost]
		public async Task<IActionResult> UpdateDriveType(int id, string name)
		{
			await _driveTypeRepo.UpdateDriveType(id, name);
			return RedirectToAction("EditDriveTypes");
		}

		public async Task<IActionResult> DeleteDriveType(int id)
		{
			await _driveTypeRepo.DeleteDriveType(id);
			return RedirectToAction("EditDriveTypes");
		}

		// CRUD for GEARBOX
		[HttpPost]
		public async Task<IActionResult> AddGearBox(GearBox gearBox)
		{
			await _gearBoxRepo.AddGearBox(gearBox);
			return RedirectToAction("EditGearBoxes");
		}

		[HttpPost]
		public async Task<IActionResult> UpdateGearBox(int id, string name)
		{
			await _gearBoxRepo.UpdateGearBox(id, name);
			return RedirectToAction("EditGearBoxes");
		}

		public async Task<IActionResult> DeleteGearBox(int id)
		{
			await _gearBoxRepo.DeleteGearBox(id);
			return RedirectToAction("EditGearBoxes");
		}

		// CRUD for ENGINE TYPES
		[HttpPost]
		public async Task<IActionResult> AddEngineType(EngineType engineType)
		{
			await _engineTypeRepo.AddEngineType(engineType);
			return RedirectToAction("EditEngineTypes");
		}

		[HttpPost]
		public async Task<IActionResult> UpdateEngineType(int id, string name)
		{
			await _engineTypeRepo.UpdateEngineType(id, name);
			return RedirectToAction("EditEngineTypes");
		}

		public async Task<IActionResult> DeleteEngineType(int id)
		{
			await _engineTypeRepo.DeleteEngineType(id);
			return RedirectToAction("EditEngineTypes");
		}
	}
}
