using Zeeyo.Service.Commons.Attributes;

namespace Zeeyo.Service.DTOs.Teachers.Teachers;

public class TeacherForUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TeacherSpecialization { get; set; }

    [PhoneNumberAttribute]
    public string PhoneNumber { get; set; }
    public string TelegramUserName { get; set; }
    public DateTime DateOfBirth { get; set; }

    [CustomEmailAddressAttribute]
    public string Email { get; set; }
    public long BranchId { get; set; }
}
