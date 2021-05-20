using System;

namespace TestMVCApp.libs.Data.Entities
{

    public class OrderItem
    {
        public string Id { get; set; }

        public OrderItem()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string OrderId { get; set; }
        public int Quantity { get; set; }

        public string ProductId { get; set; }

        public float DiscountRate { get; set; } // İndirim Oranı


        // navigation property olarak kullanıcağız.
        public Order Order { get; set; }
        public Product Product { get; set; }

    }

}