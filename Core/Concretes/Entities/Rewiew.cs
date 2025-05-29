using Core.Abstract.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concretes.Entities
{
    [Table("rewiews", Schema = "production")]
    public class Rewiew:BaseEntity
    {
        [Required]
        public required int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        [Required]
        public required string MemberId { get; set; }
        public virtual Member? Member { get; set; }
        [Required]
        [Range(1,5)]
        public required int Star { get; set; } = 0;
        public  string? Comment { get; set; }
    }
}
