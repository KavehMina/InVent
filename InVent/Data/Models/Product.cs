using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InVent.Data.Models
{
    [Table("Products")]
    public class Product : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Name")]
        public string? Name { get; set; }

        [Column("Grade")]
        public string? Grade { get; set; }

        [Column("RefineryId"), ForeignKey("Refinery_id")] // Foreign Key
        public Guid? RefineryId { get; set; }

        [ForeignKey("RefineryId")]  //Navigation
        public Refinery? Refinery { get; set; }
    }
    public class ProductDTO : IEntity
    {
        public Guid Id { get; set; }

        public required string Name { get; set; }

        public required string Grade { get; set; }

        public Guid? RefineryId { get; set; }
    }
}
