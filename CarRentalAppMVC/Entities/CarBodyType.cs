using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRentalAppMVC.Entities
{
	public class CarBodyType
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public byte Id { get; set; }

		[Required, Column(TypeName = "varchar(31)")]
		public string CarBodyTypeName { get; set; }
		public virtual ICollection<Car> Cars { get; set; }
	}
}
