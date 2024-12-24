using Zeeyo.Service.Commons.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Zeeyo.Service.DTOs.Logins;

public class LoginForCreationDto
{
    [Required(ErrorMessage = "Telefon raqamni kiriting"), PhoneNumberAttribute]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Parolni kiriting"), StrongPasswordAttribute]
    public string Password { get; set; }
}
