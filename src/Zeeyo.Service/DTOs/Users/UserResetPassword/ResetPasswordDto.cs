namespace Zeeyo.Service.DTOs.Users.UserResetPassword;

public class ResetPasswordDto
{
    public string PhoneNumberOrEmail { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
