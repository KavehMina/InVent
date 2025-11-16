using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InVent.Data.Models
{
    [Table("Customs")]
    public class Customs : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Number"), Required]
        public required int Number { get; set; }

        [Column("Name"), Required]
        public required string Name { get; set; }
    }
}
