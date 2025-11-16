using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InVent.Data.Models
{
    [Table("Carriers")]
    public class Carrier : IEntity
    {
        [Key]
        public Guid Id { get; set; }


        [Column("Name"), Required]
        public required string Name { get; set; }


        [Column("Phone"), Required]
        public required string Phone { get; set; }


        [Column("BankNumber")]
        public string? BankNumber { get; set; }


        //[Column("CarrierBankId"), ForeignKey("Bank_id")]
        [Column("CarrierBankId")]
        public Guid? CarrierBankId { get; set; }


        //CONSTRAINT [FK_Carriers_CarrierBankId] FOREIGN KEY ([CarrierBankId]) REFERENCES [dbo].[Banks] ([Id])
        //[ForeignKey("CarrierBankId")]
        [ForeignKey(nameof(CarrierBankId))]
        public Bank? Bank { get; set; }
    }

    //public class CarrierViewModel :Carrier
    //{
    //    public Bank? Bank {  set; get; }
    //}
}
