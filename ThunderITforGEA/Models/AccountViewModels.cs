using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ThunderITforGEA.Models
{
    public class ExternalLoginConfirmationViewModel
    {
       
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Obecne hasło")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nowe hasło")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Powtórz nowe hasło")]
        [Compare("NewPassword", ErrorMessage = "Hasła nie są identyczne.")]
        public string ConfirmPassword { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string login { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Login")]
        
        public string login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Display(Name = "Pamiętaj mnie?")]
        public bool RememberMe { get; set; }
        [Display(Name = "Wybierz język")]
        public string language { get; set; }
    }

    public class RegisterViewModel
    {
     
        public string login { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Hasło musi być dłuższe niż {2} znaków.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
   //     [Compare("Password", ErrorMessage = "Hasła nie są identyczne.")]
        public string ConfirmPassword { get; set; }
         [Display(Name = "Imię")]
         [StringLength(100, ErrorMessage = "Imię musi mieć conajmniej {2} litery.", MinimumLength = 2)]
        public string imie { get; set; }
         [Display(Name = "Nazwisko")]
         [Required]
        public string nazwisko { get; set; }
         [Display(Name = "Firma")]
        public string firma { get; set; }
         [Display(Name = "Adres")]
        public string adres { get; set; }
         [Display(Name = "Miasto")]
        public string miasto { get; set; }
         [Display(Name = "Kraj")]
        public string kraj { get; set; }
         [Display(Name = "Telefon")]
         [DataType(DataType.PhoneNumber)]
        public string telefon { get; set; }
         [Display(Name = "Email")]
         [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Stanowisko")]
         public string stanowisko { get; set; }
        [Display(Name = "Pion")]
         public string segment { get; set; }
        [Display(Name = "Komórka organizacyjna")]
        public string segment2 { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string login { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "Login")]
        public string login { get; set; }
    }
}
