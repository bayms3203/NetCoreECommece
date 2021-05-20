using System.Text.Json;
using Microsoft.AspNetCore.Http;


namespace TestMVCApp.libs.Sesion
{

    public class CartSessionService : ICartSessionService
    {
        // aşağıdaki IHttpContextAccessor interface ile Session bilgilerine erişim sağlayabiliriz.
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            
        }


        public void Set(string key, Cart cart)
        {


            if (cart.CarItems.Count > 0)
            {
                var cartJson = JsonSerializer.Serialize(cart); // bunu json tipinde serialize et
                                                                     // cart tipinde json string çevir demek
                _httpContextAccessor.HttpContext.Session.SetString(key, cartJson);
                // git bunu session ile rame yaz, sunucunun sessionda key ismi ile tut.
            }

        }

        public Cart Get(string key)
        {

            //json formatında key değerinin valuesunu getirir.
            string cartJson = _httpContextAccessor.HttpContext.Session.GetString(key);

            if (string.IsNullOrEmpty(cartJson))
            {
                return new Cart();
            }

            // string değeri Cart Nesnesine JsonSerializer ile çevireceğiz.
            // json stringden objeye dönüştür.
            return System.Text.Json.JsonSerializer.Deserialize<Cart>(cartJson);



        }

        public void Remove(string key)
        {
            _httpContextAccessor.HttpContext.Session.Remove(key);
        }
    }

}