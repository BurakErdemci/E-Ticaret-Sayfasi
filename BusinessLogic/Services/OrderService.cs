using Core.Abstract.IService;
using Core.Concretes.Entities;
using Core.Abstract;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Utilities.Helpers;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderHeader?> CheckoutAsync(string memberId)
        {
            var cart = (await _unitOfWork.CartRepository.ReadManyAsync(
                c => c.MemberId == memberId && c.CartStatus == CartStatus.ACTIVE, "Items.Product")).FirstOrDefault();
            if (cart == null || cart.Items.Count == 0)
                return null;

            var order = new OrderHeader
            {
                MemberId = memberId,
                OrderStatus = OrderStatus.PENDING,
                TotalDue = 0,
                TotalDiscount = 0,
                TotalText = 0,
                OrderDetails = new List<OrderDetail>()
            };

            foreach (var item in cart.Items.ToList())
            {
                var product = await _unitOfWork.ProductRepository.ReadByIdAsync(item.ProductId);
                if (product == null)
                {
                    await _unitOfWork.CartItemRepository.DeleteAsync(item);
                    cart.Items.Remove(item);
                    continue;
                }
                order.OrderDetails.Add(new OrderDetail
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product.ListPrice,
                    Discount = 0,
                    Tax = 0,
                    LineTotal = product.ListPrice * item.Quantity
                });
                order.TotalDue += product.ListPrice * item.Quantity;
            }

            if (order.OrderDetails.Count == 0)
                return null;

            await _unitOfWork.OrderHeaderRepository.CreateAsync(order);
            cart.CartStatus = CartStatus.COMPLETED;
            cart.Items.Clear();
            await _unitOfWork.CommitAsync();
            return order;
        }

        public async Task<IEnumerable<OrderHeader>> GetOrdersAsync(string memberId)
        {
            var orders = await _unitOfWork.OrderHeaderRepository.ReadManyAsync(o => o.MemberId == memberId, "OrderDetails.Product");
            return orders;
        }
    }
} 