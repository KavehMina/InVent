using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InVent.Data.Models
{
    [Table("Tankers")]
    public class Tanker : IEntity
    {
        [Key]
        public Guid Id { get; set; }


        [Column("Number")]
        public string Number { get; set; }


        [Column("DriverName")]
        public string DriverName { get; set; }


        [Column("DriverPhone")]
        public string DriverPhone { get; set; }


        [Column("DriverBankNumber")]
        public string? DriverBankNumber { get; set; }


        [Column("DriverBankId"), ForeignKey("Bank_id")]
        public Guid? DriverBankId { get; set; }


        [Column("OwnerName")]
        public string? OwnerName { get; set; }


        [Column("OwnerPhone")]
        public string? OwnerPhone { get; set; }


        [Column("OwnerBankNumber")]
        public string? OwnerBankNumber { get; set; }


        [Column("OwnerBankId"), ForeignKey("Bank_id")]
        public Guid? OwnerBankId { get; set; }


        [Column("CargoType")]
        public string CargoType { get; set; }


        [ForeignKey("DriverBankId")]
        public Bank? DriverBank { get; set; }


        [ForeignKey("OwnerBankId")]
        public Bank? OwnerBank { get; set; }
    }

    public class TankerDTO : IEntity
    {
        public Guid Id { get; set; }
        public required string Number { get; set; }
        public required string DriverName { get; set; }
        public required string DriverPhone { get; set; }
        public string? DriverBankNumber { get; set; }
        public Guid? DriverBankId { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerPhone { get; set; }
        public string? OwnerBankNumber { get; set; }
        public Guid? OwnerBankId { get; set; }
        public required string CargoType { get; set; }
    }

    //public class TankerResponseModel
    //{
    //    public required bool Success { get; set; }
    //    public List<Tanker>? Tankers { get; set; }
    //    public required string Message { get; set; }
    //}

    public class TankerViewModel : Tanker
    {
        public string? DriverBankName { get; set; }
        public string? OwnerBankName { get; set; }
    }

}
