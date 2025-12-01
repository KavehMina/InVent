using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InVent.Data.Models
{
    [Table("Projects")]
    public class Project : IEntity
    {
        [Key]
        public Guid Id { get; set; }



        [Column("Number")]
        public int Number { get; set; }



        [Column("ProjectWeight")]
        public int ProjectWeight { get; set; }


        [Column("PackageCount")]
        public int PackageCount { get; set; }


        [Column("Status")]
        public bool Status { get; set; }



        [Column("CustomerId")] //FK
        public Guid CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))] //Navigation
        public Customer? Customer { get; set; }



        [Column("PackageId")] //FK
        public Guid PackageId { get; set; }

        [ForeignKey(nameof(PackageId))] //Navigation
        public Package? Package { get; set; }



        [Column("ProductId")] //FK
        public Guid ProductId { get; set; }

        [ForeignKey(nameof(ProductId))] //Navigation
        public Product? Product { get; set; }



        [Column("PortId")] //FK
        public Guid PortId { get; set; }

        [ForeignKey(nameof(PortId))] //Navigation
        public Port? Port { get; set; }


        [Column("CustomsId")] //FK
        public Guid CustomsId { get; set; }

        [ForeignKey(nameof(CustomsId))] //Navigation
        public Customs? Customs { get; set; }

    }

    //save edit
    public class ProjectDTO : IEntity
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public int ProjectWeight { get; set; }
        public int PackageCount { get; set; }
        public required bool Status { get; set; }
        public required Guid CustomerId { get; set; }//FK
        public required Guid PackageId { get; set; }//FK
        public required Guid ProductId { get; set; }//FK       
        public required Guid PortId { get; set; } //FK
        public required Guid CustomsId { get; set; }//FK

    }

    //[Table("Projects")]
    //public class Project : IEntity
    //{
    //    [Key]
    //    public Guid Id { get; set; }



    //    [Column("Number"), Required]
    //    public int Number { get; set; }



    //    [Column("ProjectWeight"), Required]
    //    public int ProjectWeight { get; set; }



    //    [Column("Status"), Required]
    //    public required bool Status { get; set; }



    //    [Column("CustomerId"), Required] //FK
    //    public required Guid CustomerId { get; set; }

    //    [ForeignKey(nameof(CustomerId))] //Navigation
    //    public required Customer Customer { get; set; }



    //    [Column("PackageId"), Required] //FK
    //    public required Guid PackageId { get; set; }

    //    [ForeignKey(nameof(PackageId))] //Navigation
    //    public required Package Package { get; set; }



    //    [Column("ProductId"), Required] //FK
    //    public required Guid ProductId { get; set; }

    //    [ForeignKey(nameof(ProductId))] //Navigation
    //    public required Product Product { get; set; }



    //    [Column("PortId"), Required] //FK
    //    public required Guid PortId { get; set; }

    //    [ForeignKey(nameof(PortId))] //Navigation
    //    public required Port Port { get; set; }


    //    [Column("CustomsId"), Required] //FK
    //    public required Guid CustomsId { get; set; }

    //    [ForeignKey(nameof(CustomsId))] //Navigation
    //    public required Customs Customs { get; set; }

    //}
}
