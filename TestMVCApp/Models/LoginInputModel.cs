using System.ComponentModel.DataAnnotations;

namespace TestMVCApp.Models
{
    public class LoginInputModel
    {

        [EmailAddress(ErrorMessage="E-Posta formatında giriş yapınız")]
        [Required(ErrorMessage="E-Posta alanı zorunludur")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage="Parola alanı zorunludur")]
        [MinLength(8,ErrorMessage="En az 8 karakterden oluşmalıdır")]
        public string Password { get; set; }
        public bool RememberMe { get; set; } // kalıcı persistance yada session bazlı login olup olmamasını sağlarız.

    }
}