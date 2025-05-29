using Core.Abstract.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concretes.Entities
{
    [Table("brands", Schema = "production")]
    public class Brand:BaseEntity
    {
        [Required]
        public required string Name { get; set; }
        public  string? Description { get; set; }
        public string? LogoImagePath { get; set; }
        public virtual ICollection<Product> Products { get; set; } = [];


    }
}
