using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InVent.Data.Models
{
    [Table("Dispatches")]
    public class Dispatch : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Date")]
        public DateTime? Date { get; set; }

        [Column("DriverName")]
        public string? DriverName { get; set; }

        [Column("DriverNationalCode")]
        public string? DriverNationalCode { get; set; }

        [Column("DriverPhone")]
        public string? DriverPhone { get; set; }

        [Column("DriverBankNumber")]
        public string? DriverBankNumber { get; set; }

        [Column("DriverBank")]
        public string? DriverBank { get; set; }

        [Column("NumberPlate")]
        public string? NumberPlate { get; set; }

        [Column("FullWeight")]
        public int FullWeight { get; set; }

        [Column("EmptyWeight")]
        public int EmptyWeight { get; set; }

        [Column("PackageCount")]
        public int PackageCount { get; set; }

        [Column("Fare")]
        public int Fare { get; set; }

        [Column("IsExport")]
        public bool IsExport { get; set; }

        [Column("IsDischarged")]
        public bool IsDischarged { get; set; }

        [Column("IsPaid")]
        public bool IsPaid { get; set; }

        [Column("InternationalNumber1")]
        public string? InternationalNumber1 { get; set; }

        [Column("InternationalNumber2")]
        public string? InternationalNumber2 { get; set; }

        [Column("LastModifiedOn")]
        public DateTime? LastModifiedOn { get; set; }

        [Column("Description")]
        public string? Description { get; set; }

        [Column("BookingId")] //FK
        public Guid BookingId { get; set; }

        [ForeignKey(nameof(BookingId))] //Navigation
        public Booking? Booking { get; set; }



        [Column("CarrierId")] //FK
        public Guid CarrierId { get; set; }

        [ForeignKey(nameof(CarrierId))] //Navigation
        public Carrier? Carrier { get; set; }



        [Column("PortId")] //FK
        public Guid PortId { get; set; }

        [ForeignKey(nameof(PortId))] //Navigation
        public Port? Port { get; set; }



        [Column("CustomsId")] //FK
        public Guid CustomsId { get; set; }

        [ForeignKey(nameof(CustomsId))] //Navigation
        public Customs? Customs { get; set; }
    }
    public class DispatchDTO : IEntity
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public required string DriverName { get; set; }
        public required string DriverNationalCode { get; set; }
        public required string DriverPhone { get; set; }
        public string? DriverBankNumber { get; set; }
        public string? DriverBank { get; set; }
        public required string NumberPlate { get; set; }
        public required int FullWeight { get; set; }
        public required int EmptyWeight { get; set; }
        public required int PackageCount { get; set; }
        public required int Fare { get; set; }
        public required bool IsExport { get; set; }
        public required bool IsDischarged { get; set; }
        public required bool IsPaid { get; set; }
        public string? InternationalNumber1 { get; set; }
        public string? InternationalNumber2 { get; set; }
        public string? Description { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public required Guid BookingId { get; set; }//FK
        public required Guid CarrierId { get; set; }//FK
        public required Guid PortId { get; set; }//FK
        public required Guid CustomsId { get; set; }//FK

    }
}
