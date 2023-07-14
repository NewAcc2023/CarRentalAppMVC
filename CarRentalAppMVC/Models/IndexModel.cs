namespace CarRentalAppMVC.Models
{
	public class IndexModel
	{
		public int Id { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public decimal StartPrice { get; set; }

		public string ImagePath { get; set; }
	}
}
