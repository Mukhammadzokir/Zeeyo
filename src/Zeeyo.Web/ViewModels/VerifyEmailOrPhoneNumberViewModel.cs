using Zeeyo.Service.Commons.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Zeeyo.Web.ViewModels
{
    public class VerifyEmailOrPhoneNumberViewModel
    {

        [Required(ErrorMessage = "Please! Enter your email or phone number.")]
        [Display(Name = "Email or Phone number")]
        [EmailOrPhone]
        public string EmailOrPhoneNumber { get; set; }
        
    }
}