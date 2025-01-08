using Zeeyo.Domain.Enums;

namespace Zeeyo.Service.DTOs.Students.Attendances;

public class AttendanceForUpdateDto
{
    public long StudentId { get; set; }
    public long GroupId { get; set; }
    public Status Status { get; set; }
}
