using Core.Abstract.IService;
using Core.Concretes.Entities;
using Core.Abstract;
using System.Threading.Tasks;
using System.Linq;
using Utilities.Helpers;
using System;

namespace BusinessLogic.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Cart?> GetCartItemsAsync(string memberId)
        {
            var carts = await _unitOfWork.CartRepository.ReadManyAsync(c => c.MemberId == memberId && c.CartStatus == CartStatus.ACTIVE, "Items.Product");
            return carts.FirstOrDefault();
        }

        public async Task AddToCartAsync(string memberId, int productId, int quantity = 1)
        {
            var product = await _unitOfWork.ProductRepository.ReadByIdAsync(productId);
            if (product == null)
                throw new Exception("Eklenmek istenen ürün bulunamadı!");

            var cart = (await _unitOfWork.CartRepository.ReadManyAsync(c => c.MemberId == memberId && c.CartStatus == CartStatus.ACTIVE, "Items"))?.FirstOrDefault();
            if (cart == null)
            {
                cart = new Cart { MemberId = memberId, CartStatus = CartStatus.ACTIVE, Active = true };
                await _unitOfWork.CartRepository.CreateAsync(cart);
                await _unitOfWork.CommitAsync();
            }

            // Sepetteki silinmiş ürüne referanslı CartItem'ları hem veritabanından hem koleksiyondan sil
            foreach (var item in cart.Items.ToList())
            {
                var prod = await _unitOfWork.ProductRepository.ReadByIdAsync(item.ProductId);
                if (prod == null)
                {
                    await _unitOfWork.CartItemRepository.DeleteAsync(item);
                    cart.Items.Remove(item);
                }
            }

            // Duplicate CartItem varsa fazlalarını sil
            var itemsWithSameProduct = cart.Items.Where(i => i.ProductId == productId).ToList();
            if (itemsWithSameProduct.Count > 1)
            {
                foreach (var duplicate in itemsWithSameProduct.Skip(1))
                {
                    await _unitOfWork.CartItemRepository.DeleteAsync(duplicate);
                    cart.Items.Remove(duplicate);
                }
            }

            // Ürün gerçekten varsa ekle
            var itemInCart = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (itemInCart == null && product != null)
            {
                itemInCart = new CartItem { ProductId = productId, Quantity = quantity, CartId = cart.Id, Active = true };
                cart.Items.Add(itemInCart);
            }
            else if (itemInCart != null)
            {
                itemInCart.Quantity += quantity;
            }
            await _unitOfWork.CommitAsync();
        }

        public async Task RemoveFromCartAsync(string memberId, int productId)
        {
            var cart = (await _unitOfWork.CartRepository.ReadManyAsync(c => c.MemberId == memberId && c.CartStatus == CartStatus.ACTIVE, "Items"))?.FirstOrDefault();
            if (cart == null) return;
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Items.Remove(item);
            }
            await _unitOfWork.CommitAsync();
        }

        public async Task ClearCartAsync(string memberId)
        {
            var cart = (await _unitOfWork.CartRepository.ReadManyAsync(c => c.MemberId == memberId && c.CartStatus == CartStatus.ACTIVE, "Items"))?.FirstOrDefault();
            if (cart == null) return;
            cart.Items.Clear();
            await _unitOfWork.CommitAsync();
        }
    }
} 