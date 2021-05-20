namespace TestMVCApp.libs.API
{
    public interface ICardStore
    {
         CreditCardModel GetCardByNumber(string cardNumber);
    }
}