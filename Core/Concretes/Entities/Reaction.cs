using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Helpers;

namespace Core.Concretes.Entities
{
    [Table("reactions", Schema = "production")]
    public class Reaction
    {
      
        [Key]
        public int Id { get; set; }
        [Required]
        public required int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        [Required]
        public required string MemberId { get; set; }
        public virtual Member? Member { get; set; }
        [Required]
        public required ReactionType ReactionType { get; set; } 

    }
}
