using Core.Abstract.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Helpers;

namespace Core.Concretes.Entities
{
    [Table("carts",Schema ="sales")]
    public class Cart:BaseEntity {

        [Required]
        public required string MemberId { get; set; }
        public virtual Member? Member { get; set; }
        public virtual ICollection<CartItem> Items { get; set; } = [];
        public virtual CartStatus CartStatus { get; set; }



    }

}
