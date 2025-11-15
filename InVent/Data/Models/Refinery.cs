using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InVent.Data.Models
{
    [Table("Refineries")]
    public class Refinery : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Name"), Required]
        public required string Name { get; set; }

        [Column("City"), Required]
        public required string City { get; set; }
    }
}
