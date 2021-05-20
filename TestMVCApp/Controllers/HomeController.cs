using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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


    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        // bu sayafaya sadece login olanlar girebilir
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
