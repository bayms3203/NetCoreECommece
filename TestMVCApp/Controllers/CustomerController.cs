
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repo;
        private readonly UserManager<IdentityUser> _usermanager;

        public CustomerController(ICustomerRepository repo, UserManager<IdentityUser> userManager)
        {
            _repo = repo;
            _usermanager = userManager;
        }


        public async Task<IActionResult> CreateTestCustomer()
        {

            if (User.Identity.IsAuthenticated)
            {

                var authenticatedUser = await _usermanager.FindByNameAsync(User.Identity.Name);

                var customerExist = _repo.Find(authenticatedUser.Id);

                if (customerExist == null)
                {
                    var customer = new Customer();
                    customer.PhoneNumber = "05554142356";
                    customer.DefaultShippingAddress = "Beyoğlu/ Istanbul";
                    customer.Id = authenticatedUser.Id;
                    customer.FullName = "Mert Alptekin";
                    _repo.Create(customer);

                    return View();

                }
            }


            return View();
        }





    }
}
