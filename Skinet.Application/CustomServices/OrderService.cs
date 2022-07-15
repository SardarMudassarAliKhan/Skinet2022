using Skinet.Core.Entities;
using Skinet.Core.Entities.OrderAggregate;
using Skinet.Core.Interfaces;
using Skinet.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skinet.Application.CustomServices
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public  ICustomerBasket _basketRepo { get; }

        public OrderService(IUnitOfWork unitOfWork, ICustomerBasket basketRepo)
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
        }


        public async Task<Order> CreateOrderAsync(
            string buyerEmail, int deliveryMethodId, 
            string basketId, Address ShippingAddress)
        {
            //Get the BasketItem
            var basket = await _basketRepo.GetBasketAsync(basketId);
            //Get All Items 
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.repository<Products>().GetByIdAsync(item.Id);
                var productItemOrdered = new ProductItemOrdered(productItem.Id, 
                    productItem.Name, productItem.PictureUrl);
                var OrderItem = new OrderItem(productItemOrdered,
                    productItem.Price,item.Quantity);
                items.Add(OrderItem);
            }
            //Delivery Method
            var deliveryMethod = await _unitOfWork.repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            //Sub Total 
            var subTotal = items.Sum(i => i.Price * i.Quantity);
            // check to see if order exists
            /*var spec = new OrderwithItemandOrderingSpecification(basket.PaymentIntentId);
            var existingOrder = await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);*/
            //Order 
            var Order = new Order(items, buyerEmail, ShippingAddress, deliveryMethod, subTotal);
            _unitOfWork.repository<Order>().Add(Order);
            //Save Order To Database

            var result = await _unitOfWork.Complete();
            if(result<=0)
            {
                return null;
            }
            //await _basketRepo.DeleteBasketAsync(basketId);
            return Order;

        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            return await _unitOfWork.repository<DeliveryMethod>().GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderwithItemandOrderingSpecification(orderId,buyerEmail);
            return await _unitOfWork.repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
           var spec = new OrderwithItemandOrderingSpecification(buyerEmail);
            return await _unitOfWork.repository<Order>().ListAsync(spec);
        }
    }
}
