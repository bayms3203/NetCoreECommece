namespace TestMVCApp.libs.API
{
    public interface ICreditCardService:ICreditValidator,ICreditAccess,ICreditLimitChecker
    {
       CreditServiceResult  MakeCreditRequest(CreditCardModel model, decimal salesPrice);
    }
}