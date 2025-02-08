using Zeeyo.Service.Commons.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Zeeyo.Service.DTOs.Students.Students;

public class StudentForCreationDto
{
    [MinLength(1), MaxLength(64)]
    public string FirstName { get; set; }

    [MinLength(1), MaxLength(64)]
    public string LastName { get; set; }

    [StrongPasswordAttribute]
    public string Password { get; set; }

    [CustomEmailAddressAttribute]
    public string? Email { get; set; }

    public string? TelegramUserName { get; set; }

    [Required]
    public long BranchId { get; set; }

    [PhoneNumberAttribute]
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
}
