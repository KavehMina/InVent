using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InVent.Data.Models
{
    [Table("Packages")]
    public class Package : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Name"), Required]
        public required string Name { get; set; }

        [Column("Volume")]
        public int? Volume { get; set; }

        [Column("Weight")]
        public int? Weight { get; set; }
    }
}
