using Zeeyo.Domain.Enums;
using Zeeyo.Domain.Entities.Users;
using Zeeyo.Domain.Entities.Courses;

namespace Zeeyo.Service.DTOs.Students.Attendances;

public class AttendanceForResultDto
{
    public long Id { get; set; }
    public long StudentId { get; set; }
    public User Student { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; }
    public Status Status { get; set; }
}
