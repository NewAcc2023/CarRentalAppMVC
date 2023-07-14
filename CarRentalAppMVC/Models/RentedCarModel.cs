using CarRentalAppMVC.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage;

namespace CarRentalAppMVC.Models
{
	public class RentedCarModel
	{
		public int Id { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public int Doors { get; set; }
		public int Seats { get; set; }
		public string ImagePath { get; set; }
		public int Year { get; set; }

		public decimal TotalPrice { get; set; }
		public string OrderCreationDatetime { get; set; }
		public string RecieveDatetime { get; set; }
		public string ReturnDatetime { get; set; }

	}
}
