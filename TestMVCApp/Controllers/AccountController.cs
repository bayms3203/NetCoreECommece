
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

    public class AccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager; // userRepository bizim için Microsoft.AspNetCore.Identity paketinde yazılmıştıtr
        private readonly SignInManager<IdentityUser> _signInManager;


        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public async Task<IActionResult> Register() {

                var user = new IdentityUser();
                user.Email = "mert.alptekin@neominal.com";
                user.UserName = "mert.alptekin";


                var result = await _userManager.CreateAsync(user,"Neominal01?");

                // kullanıcı kayıt olduktan sonra kullanıcın e-posta adresine atılan maildeki bir şifre
                // bu şifre sayesinde mail olarak gönderilen linke basıldığında bu şifreden kullanıcın hesabının aktivasyonunu sağlıyoruz.
                
                var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);


                if(result.Succeeded) {
                    // ama burada işi hızlandırmak için token oluşturup sanki e-posta adresinden gönderilmiş gibi kod yazdık ve kullanıcıyı hesabını aşağıdaki kod ile aktive ettik.
                    await _userManager.ConfirmEmailAsync(user,confirmationToken);
                    ViewBag.Result = "Kullanıcı başarılı bir şekilde kaydedildi";

                    
                    return View();
                }

                ViewBag.Result = "Kullanıcı sisteme katdedilirken bier hata meydana geldi";

                
            return View();
             
        }


        [HttpGet]

        public IActionResult Login() {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel model) {

            if(ModelState.IsValid) {

                // db de böyle bir kullanıcı var mı
                // database işlemlerini paralelde sorguları yönetip çalıştırmak için  Microsoft.AspNetCore.Identity; askenron bazlı yazılmıştır. o yüzden async Task ile methodlarımızı asenkron kod çalışaıracak formatta yazmalıyız.

                var user = await _userManager.FindByEmailAsync(model.EmailAddress);
                var passwordConfirmed = await _userManager.CheckPasswordAsync(user,model.Password);

                if(user!= null && passwordConfirmed){
                    await _signInManager.SignInAsync(user,model.RememberMe);
                    // kullanıcı yukarıdaki kod ile sisteme giriş myaptı ve sistem yani proje artık oturum açan kullanıcı bilgisini cookie attı ve buradan okuyup kullanıcının oturum açıp açmadığını biliyor.

                    return Redirect("/");
                }
            }


            return View();
          
        }


        [HttpGet][Authorize]
        public async Task<IActionResult> LogOut() {

            await _signInManager.SignOutAsync();
            return Redirect("/");
            
        }



       




    }
}
