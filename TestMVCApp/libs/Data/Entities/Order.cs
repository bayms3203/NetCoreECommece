using System;
using System.Collections.Generic;

namespace TestMVCApp.libs.Data.Entities
{

    public class Order
    {


        public Order()
        {
            OrderId = Guid.NewGuid().ToString();
        }

        public string OrderId { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; } // Kargolama tarihi

        public string CustomerId { get; set; } // siparişi veren müşlteri

        public string CustomerName { get; set; }

        public string ShipingAddress { get; set; } 

        public string ContactNumber { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // sipariş detay

    }

}