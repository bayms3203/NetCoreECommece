

namespace TestMVCApp.libs.Sesion
{

    public interface ISessionService
    {
        void Set(string key, Cart cart); // sessionda key bazlı değer tutmak içişn
        void Remove(string key); // ilgili session ismi ile ramden kayıtı sileriz.

        Cart Get(string key); // ramde tutulan tutulan oturum bilgisini döndürür.

    }

}
