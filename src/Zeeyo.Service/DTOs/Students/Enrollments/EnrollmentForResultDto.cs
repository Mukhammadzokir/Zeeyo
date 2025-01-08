using Zeeyo.Domain.Entities.Users;
using Zeeyo.Domain.Entities.Courses;

namespace Zeeyo.Service.DTOs.Students.Enrollments;

public class EnrollmentForResultDto
{
    public long Id { get; set; }
    public long StudentId { get; set; }
    public User Student { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; }
    public DateTime EnrollmentDate { get; set; }
}
