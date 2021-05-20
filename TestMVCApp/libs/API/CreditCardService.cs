using System;

namespace TestMVCApp.libs.API
{

    public enum CreditServiceResult {

        Paid,
        NoLimit,
        NoOnline,
        Expired

    }

    public class CreditCardService: ICreditCardService
    {

        private ICardStore _cardStore;

        public CreditCardService(ICardStore cardStore)
        {
            _cardStore = cardStore;
        }

        public bool HasLimit(string cardNumber, decimal price)
        {
           var account =  _cardStore.GetCardByNumber(cardNumber);

           // bakiye satın alacağım hizmetten fazla ise limit yeterli
           if(account.Fund >= price) 
                return true;   

            return false; 
        }

        public bool IsPermittedOnlineShopping(string cardNumber)
        {
            // online alışverişe kart açık mı
            var account = _cardStore.GetCardByNumber(cardNumber);

            if(account.OnlineShopping)
                return true;

            return false;
        }

        public bool IsValid(ValidThru validThru)
        {
           var today = DateTime.Now;
           var month = today.Month; // 0-11 arası döner
           var year = today.Year; // 0-31 

           // kart kullanım durumunda mı

           if(validThru.Month < today.Month  && validThru.Year <= today.Year) 
            return false;

            return true;

        }

        // 1. valid mi expire olmamış mı
        // 2. internet kullanımına açık mı
        // 3. limit yeterli mi


        public CreditServiceResult  MakeCreditRequest(CreditCardModel model, decimal salesPrice) {

            if(HasLimit(model.CardNumber, salesPrice)) {

                var validThruModel = new ValidThru();
                var splitedValues = model.ValidThru.Split("/");

                validThruModel.Month = int.Parse(splitedValues[0]);
                validThruModel.Year = int.Parse(splitedValues[1]);

                if(IsValid(validThruModel)) {

                    if(IsPermittedOnlineShopping(model.CardNumber)) {

                        return CreditServiceResult.Paid;

                    }
                    else {
                        return CreditServiceResult.NoOnline;
                    }
                    
                }
                else {
                    return CreditServiceResult.Expired;
                }

            }
            else {
                return CreditServiceResult.NoLimit;
            }

        }
    }
}