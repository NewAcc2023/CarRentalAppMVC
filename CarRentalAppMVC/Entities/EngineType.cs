using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRentalAppMVC.Entities
{
	public class EngineType
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public byte Id { get; set; }

		[Required, Column(TypeName = "varchar(31)")]
		public string EngineTypeName { get; set; }
		public virtual ICollection<Car> Cars { get; set; }
	}
}
