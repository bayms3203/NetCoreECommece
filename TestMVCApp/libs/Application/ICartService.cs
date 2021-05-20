

namespace TestMVCApp.libs.Application
{

    public interface ICartService
    {

        // Sepete ekleme işlemi
        void AddToCart(CartItem cartItem);

        // sepetten 1 adet çıkarma işlemi
        void RemoveFromCart(string productId);
        Cart GetCart();
    }


}