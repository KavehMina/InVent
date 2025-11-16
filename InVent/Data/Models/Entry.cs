using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace InVent.Data.Models
{
    [Table("Entries")]
    public class Entry : IEntity
    {
        [Key]
        public Guid Id { get; set; }


        [Column("Date"), Required]
        public required DateTime Date { get; set; }



        [Column("Filled"), Required]
        public required int Filled { get; set; }



        [Column("Damaged"), Required]
        public required int Damaged { get; set; }



        [Column("RefineryFilled"), Required]
        public required int RefineryFilled { get; set; }



        [Column("RefineryDamaged"), Required]
        public required int RefineryDamaged { get; set; }



        [Column("RefineryNet"), Required]
        public required int RefineryNet { get; set; }



        [Column("WarehouseFilled"), Required]
        public required int WarehouseFilled { get; set; }



        [Column("WarehouseDamaged"), Required]
        public required int WarehouseDamaged { get; set; }



        [Column("WarehouseNet"), Required]
        public required int WarehouseNet { get; set; }



        //CONSTRAINT[FK_Entries_DriverName] FOREIGN KEY([DriverName]) REFERENCES[dbo].[Tankers] ([DriverName])

        [Column("DriverName"), Required] //FK
        public required string DriverName { get; set; }

        [ForeignKey(nameof(DriverName))] //Navigation
        public required string Driver { get; set; }



        [Column("ProductId"), Required] //FK
        public required Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))] //Navigation
        public required Product Product { get; set; }



        [Column("DeliveryOrderId"), Required] //FK
        public required Guid DeliveryOrderId { get; set; }

        [ForeignKey(nameof(DeliveryOrderId))] //Navigation
        public required DeliveryOrder DeliveryOrder { get; set; }



        [Column("PackageTypeId"), Required] //FK
        public required Guid PackageTypeId { get; set; }

        [ForeignKey(nameof(PackageTypeId))] //Navigation
        public required Package Package { get; set; }



        [Column("RefineryId"), Required] //FK
        public required Guid RefineryId { get; set; }

        [ForeignKey(nameof(RefineryId))] //Navigation
        public required Refinery Refinery { get; set; }

    }
}
