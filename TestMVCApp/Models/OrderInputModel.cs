using System.ComponentModel.DataAnnotations;

namespace TestMVCApp.Models
{
    public class OrderInputModel
    {
        public bool SendSMS { get; set; } // sms gönderilsin mi 
         public bool SendEmail { get; set; } // email gönderilsin mi
         public CreditDetailModel CreditDetail {get;set;} = new CreditDetailModel();
         public CustomerDetailModel CustomerDetail {get;set;} = new CustomerDetailModel();

         [Required(ErrorMessage="Sözleşmeyi kabul etmelisiniz")]
         public bool AcceptAggrement { get; set; }

    }


    public class CreditDetailModel {

        [Required(ErrorMessage="Güvenlik Kodu zorunludur")]
        public string CVV { get; set; }


        [Required(ErrorMessage="Kartın Son kullanma tarihi zorunludur")]
        public string ValidThru { get; set; }


        [Required(ErrorMessage="Kart Numaranız boş geçilemez")]
        public string Credit { get; set; }


        [Required(ErrorMessage="Kart üzerindeki isim")]
        public string CardUser { get; set; }

    }

    public class CustomerDetailModel {

        public string CustomerId { get; set; }

        [Required(ErrorMessage="Adınızı soyadaınız girmek zorundasınız")]
        public string FullName { get; set; }

        [Required(ErrorMessage="Kargo Adresini girmek zorundasınız")]
        public string ShipAddress { get; set; }

        [Required(ErrorMessage="Telefon Numarası")]
        [Phone(ErrorMessage="Telefon formatına uygun olmalıdır")]
        public string PhoneNumber { get; set; }

    }
}