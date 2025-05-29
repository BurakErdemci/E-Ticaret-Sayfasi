using Core.Concretes.Entities;

using Utilities.Generics;

namespace Core.Abstract.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {

    }
    public interface ICategoryRepository : IRepository<Category>
    {

    }
    public interface IBrandRepository : IRepository<Brand>
    {

    }
    public interface IAttributeRepository : IRepository<ProductAttribute>
    {

    }
    public interface IMediaGalleryRepository : IRepository<ProductMedia>
    {

    }
    public interface IReactionRepository : IRepository<Reaction>
    {

    }
    public interface IRewiewRepository : IRepository<Rewiew>
    {

    }
    public interface ITagRepository : IRepository<Tags>
    {

    }
    public interface ICartRepository : IRepository<Cart>
    {

    }
    public interface ICartItemRepository : IRepository<CartItem>
    {

    }
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {

    }
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {

    }

}
