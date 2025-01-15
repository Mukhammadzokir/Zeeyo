using Zeeyo.Service.Commons.Attributes;

namespace Zeeyo.Service.DTOs.Students.Students;

public class StudentForUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TelegramUserName { get; set; }
    public long UserId { get; set; }
    public long BranchId { get; set; }
    public long EnrollmentId { get; set; }

    [PhoneNumberAttribute]
    public string PhoneNumber { get; set; }

    [CustomEmailAddressAttribute]
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
}
