using Core.Abstract.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities.Helpers;

namespace Core.Concretes.Entities
{
    [Table("mediaLibrary", Schema = "production")]
    public class ProductMedia:BaseEntity
    {
        [Required]
        public required int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        [Required]
        public required string MediaPath { get; set; }
        [Required]
        public required Mediatype Mediatype { get; set; }
    }
}
