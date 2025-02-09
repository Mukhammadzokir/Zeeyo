using System.ComponentModel.DataAnnotations;
using Zeeyo.Service.Commons.Attributes;

namespace Zeeyo.Service.DTOs.Users.UserResetPassword;

public class ResetPasswordDto
{

    public string PhoneNumberOrEmail { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    [Display(Name = "New Password")]
    [StrongPasswordAttribute]
    [Compare("ConfirmPassword", ErrorMessage = "Password does not match.")]

    public string NewPassword { get; set; }

    [Required(ErrorMessage = "ConfirmPassword is required.")]
    [DataType(DataType.Password)]
    [StrongPasswordAttribute]
    [Display(Name = "Confirm New Password")]
    public string ConfirmPassword { get; set; }
}
