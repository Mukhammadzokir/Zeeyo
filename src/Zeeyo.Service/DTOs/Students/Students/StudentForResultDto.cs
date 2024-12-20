using Zeeyo.Domain.Entities.Users;
using Zeeyo.Service.DTOs.Payments;
using Zeeyo.Domain.Entities.Branches;
using Zeeyo.Service.DTOs.Students.Attendances;
using Zeeyo.Service.DTOs.Students.Enrollments;

namespace Zeeyo.Service.DTOs.Students.Students;

public class StudentForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TelegramUserName { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string IsGraduated { get; set; }
    public ICollection<PaymentForResultDto> Payments { get; set; }
    public ICollection<EnrollmentForResultDto> Courses { get; set; }
    public StudentProfilePhotoForResultDto ProfilePhoto { get; set; }
    public ICollection<AttendanceForResultDto> Attendances { get; set; }
}
