using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InVent.Data.Models
{
    [Table("Bookings")]
    public class Booking : IEntity
    {
        [Key]
        public Guid Id { get; set; }


        [Column("Number")]
        public int Number { get; set; }


        [Column("ContainerType")]
        public string ContainerType { get; set; }


        [Column("ContainerCount")]
        public int ContainerCount { get; set; }


        [Column("PackingCount")]
        public int PackingCount { get; set; }


        [Column("Destination")]
        public string? Destination { get; set; }


        [Column("ShippingLine")]
        public string? ShippingLine { get; set; }


        [Column("Forwarder")]
        public string? Forwarder { get; set; }


        [Column("IsPaymentRecieved")]
        public bool IsPaymentRecieved { get; set; }


        [Column("IsDelivered")]
        public bool IsDelivered { get; set; }



        [Column("ProjectId")] //FK
        public Guid ProjectId { get; set; }


        [ForeignKey(nameof(ProjectId))]  //Navigation
        public Project? Project { get; set; }
    }

    public class BookingDTO : IEntity
    {
        public Guid Id { get; set; }
        public required int Number { get; set; }
        public required string ContainerType { get; set; }
        public required int ContainerCount { get; set; }
        public required int PackingCount { get; set; }
        public required Guid ProjectId { get; set; }//FK
        public string? Destination { get; set; }
        public string? ShippingLine { get; set; }
        public string? Forwarder { get; set; }
        public bool IsPaymentRecieved { get; set; }
        public bool IsDelivered { get; set; }
    }
}
