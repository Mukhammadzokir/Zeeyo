using Zeeyo.Service.Commons.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Zeeyo.Service.DTOs.Logins;

public class LoginForCreationDto
{
    [Required(ErrorMessage = "Enter the your email or phone number"), EmailOrPhone]
    [Display(Name = "Phone number Or Email")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Enter the password"), StrongPasswordAttribute]
    public string Password { get; set; }
}
