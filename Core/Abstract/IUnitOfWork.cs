using Core.Abstract.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstract
{
    public interface IUnitOfWork:IAsyncDisposable
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IBrandRepository BrandRepository { get; }
        ICartRepository CartRepository { get; }
        ICartItemRepository CartItemRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        IOrderHeaderRepository OrderHeaderRepository { get; }
        IMediaGalleryRepository MediaGalleryRepository { get; }
        ITagRepository TagRepository { get; }
        IReactionRepository ReactionRepository { get; }
        IRewiewRepository   RewiewRepository { get; }
        IAttributeRepository AttributeRepository { get; }
        Task CommitAsync();

    }
}
