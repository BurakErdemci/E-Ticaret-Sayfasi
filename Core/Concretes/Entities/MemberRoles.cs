using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities.Helpers;

namespace Core.Concretes.Entities
{
    [Table("memberRoles", Schema = "account")]
    public class MemberRoles :IdentityRole
        {

        public  string?  Description { get; set; }
        public MemberType MemberType { get; set; } = MemberType.ORDINARY_CUSTOMER;



    }

}
