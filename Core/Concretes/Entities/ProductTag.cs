using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concretes.Entities
{
    [Table("productTags" , Schema ="production")]
    public class ProductTag
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        [Key, Column(Order = 1)]
        public  int TagId { get; set; }
        public virtual Tags? Tag { get; set; }

    }
}
