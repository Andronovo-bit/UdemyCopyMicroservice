using Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Order.Domain.OrderAggregate
{
    public class OrderItem: Entity
    {
        public OrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            Price = price;
        }

        public string ProductId { get; private set; }  
        public string ProductName { get; private set; }  
        public string PictureUrl { get; private set; }  
        public Decimal Price { get; private set; }

        public void UpdateOrderItem(string productName, string pictureURL, decimal price)
        {
            ProductName = productName;
            PictureUrl = pictureURL;
            Price = price; 
        }
    }
}
