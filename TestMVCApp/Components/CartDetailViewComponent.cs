using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using TestMVCApp.libs.Application;

namespace TestMVCApp.Components
{
   
    public class CartDetailViewComponent: ViewComponent
    {
        private readonly ICartService _cartService;

        public CartDetailViewComponent(ICartService cartService)
        {
            _cartService = cartService;
        }

        
        public async Task<IViewComponentResult> InvokeAsync() 
        {

            var cart = _cartService.GetCart();

            // cartsessiondan gelen data view bu vievcomponent içerisinde çeklip gönderilecek.
            // yani bu view component Shared/Components/CartDetail.cshtml dosyasına cart modelini taşıyacak. oraya yönlenicek

            return await Task.FromResult(View(cart));
        }

    }
}