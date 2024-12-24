using Zeeyo.Domain.Entities.Branches;
using Zeeyo.Service.DTOs.Teachers.TeacherCourses;

namespace Zeeyo.Service.DTOs.Teachers.Teachers;

public class TeacherForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TeacherSpecialization { get; set; }
    public string PhoneNumber { get; set; }
    public string TelegramUserName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public TeacherProfilePhotoForResultDto UserProfilePhoto { get; set; }
    public ICollection<TeacherCourseForResultDto> TeacherCourses { get; set; }
}
