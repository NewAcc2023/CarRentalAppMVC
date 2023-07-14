using CarRentalAppMVC.Contexts;
using CarRentalAppMVC.Entities;
using CarRentalAppMVC.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalAppMVC.Controllers
{
	public class OrderController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly ICarRepo _carRepo;

		private readonly IRentOrderRepo _rentOrderRepo;
		private readonly IStatusRepo _statusRepo;
		public OrderController(UserManager<IdentityUser> userManager, ICarRepo carRepo, IRentOrderRepo rentOrderRepo, IStatusRepo statusRepo)
		{
			_userManager = userManager;
			_carRepo = carRepo;
			_rentOrderRepo = rentOrderRepo;
			_statusRepo = statusRepo;
		}

		public async Task<IActionResult> CarInfo(int carId)
		{
			Car car = await _carRepo.GetAll().FirstOrDefaultAsync(x => x.Id == carId);
			if (car == null)
			{
				return RedirectToAction("SomethingWentWrong", "Home");
			}
			return View(car);
		}

		public async Task<IActionResult> Order(DateTime recieveDate, DateTime returnDate, int carId, decimal carPrice)
		{
			if (!(User.Identity.IsAuthenticated))
			{
				TempData["message"] = " - Not authorized!";
				return Redirect($"CarInfo?carId={carId}");
			}
			else if (!User.IsInRole("Customer"))
			{
				TempData["message"] = " - Please add your phone number in the personal cabinet!";
				return Redirect($"CarInfo?carId={carId}");
			}

			List<RentOrder> rentOrders = await _rentOrderRepo.GetAll().ToListAsync();
			List<Status> orderStatuses = await _statusRepo.GetAll().ToListAsync();
			List<Car> cars = await _carRepo.GetAll().ToListAsync();
			List<RentOrder> lockedCars = rentOrders.Where(x => x.ReturnDatetime > DateTime.Now && x.StatusId != orderStatuses.FirstOrDefault(x => x.StatusName == "Cancelled").Id && x.StatusId != orderStatuses.FirstOrDefault(x => x.StatusName == "Finished").Id).ToList();

			if (lockedCars.Find(x => x.CarId == carId) != null)
			{
				List<RentOrder> thisCarRentORders = rentOrders.Where(x => x.CarId == carId).ToList();
				foreach(var thisCarRentORder in thisCarRentORders)
				{
					if(thisCarRentORder.RecieveDatetime < recieveDate && recieveDate < thisCarRentORder.ReturnDatetime ||
					thisCarRentORder.RecieveDatetime < returnDate && returnDate < thisCarRentORder.ReturnDatetime ||
					recieveDate < thisCarRentORder.RecieveDatetime && returnDate > thisCarRentORder.ReturnDatetime)
					{
						TempData["message"] = $" - The car is already in use until {lockedCars.Last(x => x.CarId == carId).ReturnDatetime}!"; //date

						return Redirect($"CarInfo?carId={carId}");
					}
				}
			}

			var user = await _userManager.GetUserAsync(User);

			if ((returnDate - recieveDate).TotalHours < 1)
			{
				TempData["message"] = " - Order should be at least for 1 hour!";
				return Redirect($"CarInfo?carId={carId}");
			}

			if (returnDate < recieveDate)
			{
				TempData["message"] = " - Return date should be bigger than recieve date!";
				return Redirect($"CarInfo?carId={carId}");
			}

			decimal total;
			if ((returnDate - recieveDate).Days < 1)
			{
				TempData["message"] = " - Order was less than for a day, so you'll pay for the 1 whole day!";
				total = 1 * carPrice;
			}
			else
			{
				total = (returnDate - recieveDate).Days * carPrice;
			}
			//add to orders
			RentOrder rentOrder = new RentOrder()
			{
				UserId = user.Id,
				CarId = carId,
				OrderCreationDatetime = DateTime.Now,
				RecieveDatetime = recieveDate,
				ReturnDatetime = returnDate,
				StatusId = orderStatuses.First(x => x.StatusName == "Reserved").Id,
				totalPrice = total,
			};
			_rentOrderRepo.CreateOrder(rentOrder);

			return View("OrderSuccess");
		}

		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Finish(int id, int page)
		{
			await _rentOrderRepo.Finish(id);
			return Redirect($"/Admin/ManageOrders?page={page}");
		}
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Cancel(int id, int page)
		{
			await _rentOrderRepo.Cancel(id);
			return Redirect($"/Admin/ManageOrders?page={page}");
		}
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> SetActive(int id, int page)
		{
			await _rentOrderRepo.SetActive(id);
			return Redirect($"/Admin/ManageOrders?page={page}");
		}



	}
}
