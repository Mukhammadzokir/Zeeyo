using Zeeyo.Domain.Commons;
using Zeeyo.Domain.Entities.Branches;
using Zeeyo.Domain.Entities.Payments;
using Zeeyo.Domain.Entities.Students;
using Zeeyo.Domain.Entities.Teachers;

namespace Zeeyo.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
    public string? Email { get; set; }
    //public string RefreshToken { get; set; }
    //public DateTime ExpireDate { get; set; }
    //public bool IsVerified { get; set; } = false;
    public DateTime? DateOfBirth { get; set; }
    public string? TelegramUserName { get; set; }
    public string? TeacherSpecialization { get; set; }
    public bool? IsStudentGraduated { get; set; } = false;
    public UserProfilePhoto? UserProfilePhoto { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }
    public ICollection<UserCode> UserCodes { get; set; }
    public ICollection<Payment> StudentPayments { get; set; }
    public ICollection<Enrollment> StudentGroups { get; set; }
    public ICollection<TeacherGroup> TeacherGroups { get; set; }
    public ICollection<Attendance> StudentAttendances { get; set; }
}
