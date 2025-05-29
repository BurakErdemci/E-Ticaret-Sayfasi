using Core.Abstract;
using Core.Abstract.IRepository;
using Data.Context;
using Data.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork:IUnitOfWork
    {

        private readonly ShopContext context;

        public UnitOfWork(ShopContext context)
        {
            this.context = context;
        }

        private IProductRepository? productRepository;
        public IProductRepository ProductRepository => productRepository??= new ProductRepository(context);


        private ICategoryRepository? categoryRepository;
        public ICategoryRepository CategoryRepository => categoryRepository??= new CategoryRepository(context);

        private IBrandRepository? brandRepository;
        public IBrandRepository BrandRepository => brandRepository??= new BrandRepository(context);


        private ICartRepository? cartRepository;
        public ICartRepository CartRepository => cartRepository??= new CartRepository(context);


        private ICartItemRepository? cartItemRepository;    
        public ICartItemRepository CartItemRepository => cartItemRepository?? new CartItemRepository(context);


        private IOrderDetailRepository? orderDetailRepository;
        public IOrderDetailRepository OrderDetailRepository => orderDetailRepository?? new OrderDetailRepository(context);


        private IOrderHeaderRepository? orderHeaderRepository;
        public IOrderHeaderRepository OrderHeaderRepository => orderHeaderRepository?? new OrderHeadersRepository(context);

        
        private IMediaGalleryRepository? mediaGalleryRepository;
        public IMediaGalleryRepository MediaGalleryRepository => mediaGalleryRepository?? new MediaGalleryRepository(context);


        private ITagRepository? tagRepository;
        public ITagRepository TagRepository => tagRepository?? new TagRepository(context);



        private IReactionRepository? reactionRepository;
        public IReactionRepository ReactionRepository => reactionRepository?? new ReactionRepository(context);

        
        private IRewiewRepository? rewiewRepository;
        public IRewiewRepository RewiewRepository => rewiewRepository?? new RewiewsRepository(context);

        private IAttributeRepository? attributeRepository;
        public IAttributeRepository AttributeRepository => attributeRepository?? new AttributesRepository(context);

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await context.DisposeAsync();
        }
    }
}
