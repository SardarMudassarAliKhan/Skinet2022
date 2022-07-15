using System;
using System.Collections.Generic;
using System.Text;

namespace Skinet.Core.Entities.OrderAggregate
{
    public class Order:BaseEntity
    {
        public Order()
        {

        }
        public Order(IReadOnlyList<OrderItem> orderItems, string buyerEmial, Address shipToAddress, 
            DeliveryMethod deliveryMethod, 
             decimal subTotal)
        {
            BuyerEmial = buyerEmial;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
        }

        public string BuyerEmial { get; set; }
        public DateTime OrderDate { get; set; }=DateTime.Now;

        public Address ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItem> OrderItems { get; set; }
        public decimal SubTotal { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public string PaymentIntentId { get; set; }
        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Price;
        }
    }
}
