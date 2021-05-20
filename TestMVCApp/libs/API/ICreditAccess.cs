namespace TestMVCApp.libs.API
{

    // bu kredi kartının internet üzerinden alış verişe açık mı
    public interface ICreditAccess
    {
         bool IsPermittedOnlineShopping(string cardNumber);
    }
}