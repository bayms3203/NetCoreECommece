

using System.Linq;
using TestMVCApp.libs.Sesion;

namespace TestMVCApp.libs.Application
{


    public class CartService : ICartService
    {

        private ICartSessionService _cartSessionService;

        public CartService(ICartSessionService cartSessionService)
        {
            _cartSessionService = cartSessionService;
        }

        // sepete ürün eklemek için
        public void AddToCart(CartItem cartItem)
        {
            // add methodu daha geliştirilecek.
            var cart = _cartSessionService.Get("CartSession");

            // daha önce aynı ürün sepete eklendiyse bu durumda sepetteki ürün adetini güncelleyeceğiz
            // yeni ekleniyorsa bu durumda yeni bir item olarak cartItemsa ekleteceğiz.

            
                 CartItem foundedCartItem = cart.CarItems.FirstOrDefault(x => x.ProductId == cartItem.ProductId);


                if (foundedCartItem != null)
                { // daha öncesinde ürün sepete konmuş

                    foundedCartItem.Quantity += cartItem.Quantity;
                }
                else
                {
                    cart.CarItems.Add(cartItem); // daha önce bu item sepete eklenmediyse
                }

                // buradaki sepet değerimiz en son olarak güncellenmiştir. ya yeni bir item gelmiş yada var olan bir item quantity artmıştır.

                _cartSessionService.Set("CartSession", cart);
            

           


        }

        // sepet bilgisini içerisinde taşır
        public Cart GetCart()
        {

            var cart = _cartSessionService.Get("CartSession");

            return cart;
        }

        // sepetten ürün silmek için
        public void RemoveFromCart(string productId)
        {
            // eğer Cart Sessionda bir item yok ise yani items Count 0 ise sessiondan sileceğiz.
            // yoksa sadece carttan çıkartıp sonra sessiona yine son cart nesnesini set edeceğiz.

            var cart = _cartSessionService.Get("CartSession");

            var cartItem = cart.CarItems.FirstOrDefault(x => x.ProductId == productId);

            cart.CarItems.Remove(cartItem);

            _cartSessionService.Set("CartSession", cart);


        }
    }

}