using Core.Abstract.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concretes.Entities
{
    [Table("media_paths", Schema = "production")]
    public class MediaPath : BaseEntity
    {
        [Required]
        public required string Path { get; set; }

        public string? AltText { get; set; }

        // Add other properties if needed, e.g., MimeType, UploadDate, etc.
    }
} 