
using Microsoft.AspNetCore.Mvc;
using TestMVCApp.libs.Application;
using TestMVCApp.libs.Data.Contexts;
using TestMVCApp.libs.Data.Entities;
using TestMVCApp.libs.Data.Repositories;
using TestMVCApp.Models;

namespace TestMVCApp.Controllers
{

    // dotnet new mvc --name=TestMVC --no-https => mvc proje açma komut
    // dotnet new web --name=TestRazorApp --no-https =>  razorpages proje açma komut


    // MVC LifeCyle
    // ilk istek controller a düşer
    // sonrasında ekranda yani view de (html,js,css olduğu sayfada) ekranda gösterilecek olan dinamik veriler (model) ile sayfalarımızı oluştururuz.

    // Home/Index
    // Home/Privacy
    // Home/Error
    // Controller/Action/Id

    public class ProductController : Controller
    {
        private readonly IProductRepository _repo;
        private readonly ICartService _cartService;

        public ProductController(IProductRepository repo, ICartService cartService)
        {
            _repo = repo;
            _cartService = cartService;
        }


        public IActionResult List()
        {
             var plist = _repo.toList();
             var cart = _cartService.GetCart();

            var model  = new ProductListVM(plist,cart);



            // var p1 = new Product();
            // p1.Name = "Ürün 1";
            // p1.ListPrice = 100;
            // p1.UnitPrice = 85;
            // p1.Stock = 10;

            // _repo.Create(p1);



            // var p2 = new Product();
            // p2.Name = "Ürün 2";
            // p2.ListPrice = 101;
            // p2.UnitPrice = 81;
            // p2.Stock = 20;

            // _repo.Create(p2);


            // var p3 = new Product();
            // p3.Name = "Ürün 3";
            // p3.ListPrice = 145;
            // p3.UnitPrice = 23;
            // p3.Stock = 30;

            // _repo.Create(p3);


            // var p4 = new Product();
            // p4.Name = "Ürün 4";
            // p4.ListPrice = 105;
            // p4.UnitPrice = 23;
            // p4.Stock = 21;

            // _repo.Create(p4);


            // var p5 = new Product();
            // p5.Name = "Ürün 5";
            // p5.ListPrice = 75;
            // p5.UnitPrice = 23;
            // p5.Stock = 21;

            // _repo.Create(p5);




           

            return View(model);
        }





    }
}
