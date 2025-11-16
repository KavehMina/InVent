using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InVent.Data.Models
{
    [Table("Projects")]
    public class Project : IEntity
    {
        [Key]
        public Guid Id { get; set; }



        [Column("Number"), Required]
        public int Number { get; set; }



        [Column("ProjectWeight"), Required]
        public int ProjectWeight { get; set; }



        [Column("Status"), Required]
        public required bool Status { get; set; }



        [Column("CustomerId"), Required] //FK
        public required Guid CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))] //Navigation
        public required Customer Customer { get; set; }



        [Column("PackageId"), Required] //FK
        public required Guid PackageId { get; set; }

        [ForeignKey(nameof(PackageId))] //Navigation
        public required Package Package { get; set; }



        [Column("ProductId"), Required] //FK
        public required Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))] //Navigation
        public required Product Product { get; set; }



        [Column("PortId"), Required] //FK
        public required Guid PortId { get; set; }

        [ForeignKey(nameof(PortId))] //Navigation
        public required Port Port { get; set; }


        [Column("CustomsId"), Required] //FK
        public required Guid CustomsId { get; set; }

        [ForeignKey(nameof(CustomsId))] //Navigation
        public required Customs Customs { get; set; }

    }
}
