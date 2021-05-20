namespace TestMVCApp.libs.API
{


    public class CreditCardModel {

        public string CardNumber { get; set; }
        public string ValidThru { get; set; }

        public string CVV { get; set; }

        public string OwnerName { get; set; }

        public decimal Fund { get; set; }

        public bool OnlineShopping {get;set;}

    }


}