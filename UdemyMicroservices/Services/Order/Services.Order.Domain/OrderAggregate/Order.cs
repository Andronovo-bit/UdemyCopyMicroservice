using Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Order.Domain.OrderAggregate
{

    //EF Core Features
    //-- Owned Types
    //-- Shadow Propery
    //-- Backing Field
    public class Order: Entity
    {
        public DateTime CreatedDate { get; private set; } //propery

        public Address Address { get; private set; }

        public string BuyerId { get; private set; }

        private readonly List<OrderItem> _orderItems; //field //backing fields

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order() { }
        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }

        public void AddOrderItem(string productId, string productName,decimal price, string pictureUrl)
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);

            if (!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);

                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
