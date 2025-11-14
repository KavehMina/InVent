using System.ComponentModel.DataAnnotations;

namespace InVent.Data.Models
{
    public interface IEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
