using System.Collections.Generic;


namespace TestMVCApp.libs.Domain
{


    public interface IEmailService
    {

        void SendEmail(EmailModel model);

    }

    public class EmailModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public List<string> cc { get; set; }
        public List<string> to { get; set; }
        public string RedirectUrl { get; set; } // butona tıklanınca bizim web sayfasıbna gelsin
    }


}