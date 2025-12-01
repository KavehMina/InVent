using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InVent.Data.Models
{
    [Table("DeliveryOrders")]
    public class DeliveryOrder : IEntity
    {
        [Key]
        public Guid Id { get; set; }


        [Column("DeliveryOrderId")]
        public string? DeliveryOrderId { get; set; }


        [Column("Weight")]
        public int Weight { get; set; }


        [Column("TankerFare")]
        public int TankerFare { get; set; }


        [Column("Status")]
        public bool Status { get; set; }


        [Column("ProjectId")] //FK
        public Guid ProjectId { get; set; }


        [ForeignKey(nameof(ProjectId))]  //Navigation
        public Project? Project { get; set; }
    }
    //save edit delete
    public class DeliveryOrderDTO : IEntity
    {
        public Guid Id { get; set; }
        public required string DeliveryOrderId { get; set; }
        public required int Weight { get; set; }
        public required int TankerFare { get; set; }
        public required bool Status { get; set; }
        public required Guid ProjectId { get; set; }//FK

    }


    //[Table("DeliveryOrders")]
    //public class DeliveryOrder : IEntity
    //{
    //    [Key]
    //    public Guid Id { get; set; }



    //    [Column("DeliveryOrderId"), Required]
    //    public required string DeliveryOrderId { get; set; }



    //    [Column("Weight"), Required]
    //    public required int Weight { get; set; }



    //    [Column("Status"), Required]
    //    public required bool Status { get; set; }



    //    [Column("ProjectId"), Required] //FK
    //    public required Guid ProjectId { get; set; }

    //    [ForeignKey(nameof(ProjectId))]  //Navigation
    //    public required Project Project { get; set; }
    //}
}
