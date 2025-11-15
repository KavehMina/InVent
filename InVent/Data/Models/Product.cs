using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InVent.Data.Models
{
    [Table("Products")]
    public class Product: IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Name"), Required]
        public required string Name { get; set; }

        [Column("Grade"), Required]
        public required string Grade { get; set; }
    }
}
