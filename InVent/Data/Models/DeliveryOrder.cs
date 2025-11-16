using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InVent.Data.Models
{
    [Table("DeliveryOrders")]
    public class DeliveryOrder : IEntity
    {
        [Key]
        public Guid Id { get; set; }



        [Column("DeliveryOrderId"), Required]
        public required string DeliveryOrderId { get; set; }



        [Column("Weight"), Required]
        public required int Weight { get; set; }



        [Column("Status"), Required]
        public required bool Status { get; set; }



        [Column("ProjectId"), Required] //FK
        public required Guid ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]  //Navigation
        public required Project Project { get; set; }
    }
}
