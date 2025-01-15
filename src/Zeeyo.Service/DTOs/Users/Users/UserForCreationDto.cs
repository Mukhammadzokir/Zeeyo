using Zeeyo.Service.Commons.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Zeeyo.Service.DTOs.Users.Users;

public class UserForCreationDto
{
    [MinLength(1), MaxLength(64)]
    public string FirstName { get; set; }

    [MinLength(1), MaxLength(64)]
    public string LastName { get; set; }

    [PhoneNumberAttribute]
    public string PhoneNumber { get; set; }

    [StrongPasswordAttribute]
    public string Password { get; set; }

    [CustomEmailAddressAttribute]
    public string Email { get; set; }

    [Required]
    public long BranchId { get; set; }
}
