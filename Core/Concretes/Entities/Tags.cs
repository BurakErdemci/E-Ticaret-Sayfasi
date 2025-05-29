using Core.Abstract.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concretes.Entities
{
    [Table("tag", Schema = "production")]
    public class Tags : BaseEntity
    {
        public  required string  Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } = [];

    }
}
