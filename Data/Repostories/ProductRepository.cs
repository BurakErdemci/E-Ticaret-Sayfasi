using Core.Abstract.IRepository;
using Core.Concretes.Entities;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Generics;

namespace Data.Repostories
{
    public class ProductRepository:Repository<Product>,IProductRepository
    {   public ProductRepository(ShopContext db):base(db)
        {

        }

    }
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ShopContext db) : base(db)
        {

        }

    }
    public class BrandRepository : Repository<Brand>, IBrandRepository
    {
        public BrandRepository(ShopContext db) : base(db)
        {

        }

    }
    public class AttributesRepository : Repository<ProductAttribute>, IAttributeRepository
    {
        public AttributesRepository(ShopContext db) : base(db)
        {

        }

    }
    public class MediaGalleryRepository : Repository<ProductMedia>, IMediaGalleryRepository
    {
        public MediaGalleryRepository(ShopContext db) : base(db)
        {

        }

    }
    public class ReactionRepository : Repository<Reaction>, IReactionRepository
    {
        public ReactionRepository(ShopContext db) : base(db)
        {

        }

    }
    public class RewiewsRepository : Repository<Rewiew>, IRewiewRepository
    {
        public RewiewsRepository(ShopContext db) : base(db)
        {

        }

    }
    public class TagRepository : Repository<Tags>, ITagRepository
    {
        public TagRepository(ShopContext db) : base(db)
        {

        }

    }
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(ShopContext db) : base(db)
        {

        }

    }
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(ShopContext db) : base(db)
        {

        }

    }
    public class OrderHeadersRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        public OrderHeadersRepository(ShopContext db) : base(db)
        {

        }

    }
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(ShopContext db) : base(db)
        {

        }

    }
}
