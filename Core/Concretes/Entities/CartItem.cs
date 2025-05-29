using Core.Abstract.BaseModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concretes.Entities
{
    [Table("cartItems", Schema = "sales")]
    public class CartItem : BaseEntity {

        public  int ProductId{ get; set; }

        public virtual Product? Product { get; set; }
        public int CartId { get; set; }
        public  virtual Cart? Cart { get; set; }
        public int Quantity { get; set; }   


    }

}
