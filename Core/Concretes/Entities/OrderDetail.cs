using Core.Abstract.BaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concretes.Entities
{
    [Table("orderDetails", Schema = "sales")]
    public class OrderDetail : BaseEntity {
        [Required]
        public required int ProductId{ get; set; }
        public virtual Product? Product { get; set; }
        public  int  Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal LineTotal{ get; set; }
        public int OrderHeaderId { get; set; }

        public virtual OrderHeader? OrderHeader { get; set; }


    }

}
