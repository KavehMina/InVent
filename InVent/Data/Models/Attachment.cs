using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InVent.Data.Models
{
    [Table("Attachments")]
    public class Attachment : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Column("FileName")]
        public required string FileName { get; set; }

        [Column("ContentType")]
        public required string ContentType { get; set; }

        [Column("FileSize")]
        public required long FileSize { get; set; }

        //[Column("FileData")]
        //public required byte[] FileData { get; set; }
        [Column("FilePath")]
        public required string FilePath { get; set; }

        [Column("ParentType")]
        public required string ParentType { get; set; }

        [Column("Category")]
        public required string Category { get; set; }

        [Column("ParentId")]
        public Guid ParentId { get; set; } 
    }


}
