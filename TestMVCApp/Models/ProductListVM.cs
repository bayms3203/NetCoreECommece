using System.Collections.Generic;
using TestMVCApp.libs.Data.Entities;

namespace TestMVCApp.Models
{
    public class ProductListVM
    {
        public List<Product> Products { get; private set; } = new List<Product>();
        public Cart Cart { get; private set; }

        // constructor üzerinden privat olarak set ediyoruz. Property set kapadık

        public ProductListVM(List<Product> plist, Cart cart)
        {
            this.Products = plist;
            this.Cart = cart;
        }

    }
}