using System.ComponentModel.DataAnnotations;

namespace Zeeyo.Web.ViewModels;

public class VerifyCodeViewModel
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    public string EmailOrPhoneNumber { get; set; }

    [Required]
    public string Code { get; set; }
}
