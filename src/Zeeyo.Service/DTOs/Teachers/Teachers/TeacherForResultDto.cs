using Zeeyo.Domain.Entities.Branches;
using Zeeyo.Domain.Entities.Users;
using Zeeyo.Service.DTOs.Teachers.TeacherCourses;

namespace Zeeyo.Service.DTOs.Teachers.Teachers;

public class TeacherForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Specialization { get; set; }
    public string PhoneNumber { get; set; }
    public string TelegramUserName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public TeacherProfilePhotoForResultDto ProfilePhoto { get; set; }
    public ICollection<TeacherCourseForResultDto> Courses { get; set; }
}
