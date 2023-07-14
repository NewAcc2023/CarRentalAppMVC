using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CarRentalAppMVC.Entities
{
    public class Car
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Brand")]
        public short BrandId { get; set; }

        public virtual Brand Brand { get; set; }

        [Required, Column(TypeName = "varchar(63)")]
        public string ModelName { get; set; }

        [Required]
        [ForeignKey("GearBox")]
        public byte GearBoxId { get; set; }

        public virtual GearBox GearBox { get; set; }
        [Required]
        [ForeignKey("EngineType")]
        public byte EngineTypeId { get; set; }
        public virtual EngineType EngineType { get; set; }
        public int EngineCapacity { get; set; }

        [Required]
        [ForeignKey("CarBodyType")]
        public byte CarBodyTypeId { get; set; }
        public virtual CarBodyType CarBodyType { get; set; }
        [Required]
        public int Doors { get; set; }

        [Required]
        public int Seats { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required, Column(TypeName = "varchar(1023)")]
        public string Description { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [ForeignKey("DriveType")]
        public byte DriveTypeId { get; set; }
        public virtual DriveType DriveType { get; set; }

        [Required, Column(TypeName = "varchar(255)")]
        public string ImagePath { get; set; }

        public virtual ICollection<RentOrder> RentOrders { get; set; }

    }
}
