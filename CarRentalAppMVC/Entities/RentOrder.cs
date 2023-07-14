using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRentalAppMVC.Entities
{
	public class RentOrder
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[ForeignKey("Car")]
		public int CarId { get; set; }
		public virtual Car Car { get; set; }

		[Required, ForeignKey("AppUser"), Column(TypeName = "nvarchar(450)")]
		public string UserId { get; set; }
		public virtual AppUser User { get; set; }

		[Required]
		public DateTime OrderCreationDatetime { get; set; }

		[Required]
		public DateTime RecieveDatetime { get; set; }

		[Required]
		public DateTime ReturnDatetime { get; set;}

		[Required]
		[ForeignKey("Status")]
		public byte StatusId { get; set; }
		public virtual Status Status { get; set; }

		[Required, Column(TypeName = "DECIMAL(9,2)")]
		public	decimal totalPrice { get; set; }
	}
}
			