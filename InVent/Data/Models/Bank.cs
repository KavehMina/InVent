using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InVent.Data.Models
{
    [Table ("Banks")]
    public class Bank : IEntity
    {
        [Key]
        public Guid Id { get; set; }
        [Column("Name"), Required]
        public required string Name { get; set; }


        //public ICollection<Tanker>? DriverTankers { get; set; }
        //public ICollection<Tanker>? OwnerTankers { get; set; }
    }
}
