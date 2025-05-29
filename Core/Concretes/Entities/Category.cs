using Core.Abstract.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concretes.Entities
{
    [Table("categories", Schema = "production")]
    public class Category : BaseEntity
    {
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? CoverImagePath { get; set; }
        public int? ParentId { get; set; }
        public virtual Category? Parent { get; set; }
        public virtual ICollection<Product> Products { get; set; } = [];
        public ICollection<Tags> Tags { get; set; } = [];
    }
}
