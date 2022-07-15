using Skinet.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Skinet.Core.Specifications
{
    public class OrderwithItemandOrderingSpecification : BaseSpecification<Order>
    {
        public OrderwithItemandOrderingSpecification(string Email):
            base(x=>x.BuyerEmial==Email)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(d => d.DeliveryMethod);
            AddOrderByDecending(od => od.OrderDate);
        }

        public OrderwithItemandOrderingSpecification(int id,string emial) 
            : base(o=>o.Id==id&&o.BuyerEmial==emial)
        {

        }
    }
}
