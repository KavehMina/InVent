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

        [Column("BankId"), ForeignKey("Bank_id")]
        public Guid? BankId { get; set; }

        //CONSTRAINT [FK_Carriers_BankId] FOREIGN KEY ([BankId]) REFERENCES [dbo].[Banks] ([Id])
        [ForeignKey("BankId")]
        public Bank? Bank { get; set; }
    }
}
