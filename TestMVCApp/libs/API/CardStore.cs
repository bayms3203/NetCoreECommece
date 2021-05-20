using System.Collections.Generic;
using System.Linq;

namespace TestMVCApp.libs.API
{
    public class CardStore: ICardStore
    {

        public CardStore()
        {
            
        }
        
       private List<CreditCardModel> GetValidCards() {

            var validCards = new List<CreditCardModel>();
            validCards.Add(new CreditCardModel {
                CardNumber = "1250-3560-4125-3667",
                ValidThru="12/25",
                CVV = "155",
                OwnerName="Hamdi",
                Fund=10000,
                OnlineShopping = true
            });

            validCards.Add(new CreditCardModel {
                CardNumber = "1250-3560-4125-3701",
                ValidThru="12/24",
                CVV = "142",
                OwnerName = "Raul",
                Fund=12000,
                OnlineShopping = true
            });


            validCards.Add(new CreditCardModel {
                CardNumber = "1250-3560-4125-3802",
                ValidThru="11/28",
                CVV = "154",
                OwnerName = "Samet",
                Fund = -500,
                OnlineShopping = false
            });


            validCards.Add(new CreditCardModel {
                CardNumber = "1250-3560-4125-3989",
                ValidThru="12/24",
                CVV = "123",
                OwnerName ="Yağmur",
                Fund = 9500,
                OnlineShopping = false
            });

            validCards.Add(new CreditCardModel {
                CardNumber = "1250-3560-4125-4011",
                ValidThru="12/23",
                CVV = "155",
                OwnerName = "Mert",
                Fund = 500,
                OnlineShopping = true
            });

            return validCards;

        }

       public CreditCardModel GetCardByNumber(string cardNumber) {
           return GetValidCards().Where(x=> x.CardNumber == cardNumber).FirstOrDefault();
       }

    }
}