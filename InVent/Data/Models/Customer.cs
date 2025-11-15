using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InVent.Data.Models
{
    [Table("Customers")]
    public class Customer :IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Name"), Required]
        public required string Name { get; set; }

    }
}
