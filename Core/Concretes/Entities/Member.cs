using Core.Abstract.BaseModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Concretes.Entities
{
    [Table("members", Schema = "account")]
    public class Member : IdentityUser 
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public  required string  LastName { get; set; }
        public string? ProfileImagePath { get; set; }
        public string? Biography { get; set; }
        [Required]
        public required DateTime DateOfBirth { get; set; }
        public  char?  Gender { get; set; }
        public virtual ICollection<Cart> Carts { get; set; } = [];
        public virtual ICollection<OrderHeader> Orders { get; set; } = [];
        public virtual ICollection<Reaction> Reactions { get; set; } = [];
        public virtual ICollection<Rewiew> Rewiews { get; set; } = [];



    }

}
