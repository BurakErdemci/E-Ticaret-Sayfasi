using Core.Concretes.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context
{
    public class ShopContext:IdentityDbContext<Member,MemberRoles,string>

    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {

        }
        #region Production Scheme
        public virtual DbSet<Product> Products { get; set; }
        public  virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<ProductAttribute> Attributes { get; set; }
        public virtual DbSet<ProductMedia> MediaGallery { get; set; }
        public virtual DbSet<Reaction> Reactions { get; set; }
        public  virtual DbSet<Rewiew> Rewiews { get; set; }
        public virtual DbSet<Tags>Tags { get; set; }
        #endregion


        #region Sales Scheme
        public  virtual DbSet<Cart> Carts { get; set; }
        public  virtual DbSet<CartItem> CartItems { get; set; }
        public  virtual DbSet<OrderHeader> OrderHeaders { get; set; }
        public  virtual DbSet<OrderDetail> OrderDetails{ get; set; }





        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("account");
            builder.Entity<IdentityUserRole<string>>(b => { b.ToTable("memberMemberRoles"); });
            builder.Entity<IdentityUserClaim<string>>(b => { b.ToTable("memberClaims"); });
            builder.Entity<IdentityUserLogin<string>>(b => { b.ToTable("memberLogins"); });
            builder.Entity<IdentityRoleClaim<string>>(b => { b.ToTable("memberRoleClaims"); });
            builder.Entity<IdentityUserToken<string>>(b => { b.ToTable("memberTokens"); });
        }

    }
}
