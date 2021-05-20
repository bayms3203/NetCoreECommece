namespace TestMVCApp.libs.API
{
    public interface ICreditLimitChecker
    {
         bool HasLimit(string cardNumber, decimal salesPrice);
    }
}