using System;
using System.Collections.Generic;

namespace TestMVCApp.libs.Data.Entities
{

    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? ListPrice { get; set; } // Liste Fiyatı
        public int? Stock { get; set; }

        public Product()
        {

        }

        // tablolar için joişn görevi gören alanlar
        public List<Order> Orders { get; set; }

    }

}
