using System.ComponentModel.DataAnnotations;
using Zeeyo.Service.Commons.Attributes;

namespace Zeeyo.Service.DTOs.Teachers.Teachers;

public class TeacherForCreationDto
{
    [MinLength(1),MaxLength(64)]
    public string FirstName { get; set; }

    [MinLength(1),MaxLength(64)]
    public string LastName { get; set; }

    [MinLength(1), MaxLength(200)]
    public string TeacherSpecialization { get; set; }

    [PhoneNumberAttribute]
    public string PhoneNumber { get; set; }

    [MinLength(1), MaxLength(64)]
    public string TelegramUserName { get; set; }
    public DateTime DateOfBirth { get; set; }

    [Required]
    public long BranchId { get; set; }
}
