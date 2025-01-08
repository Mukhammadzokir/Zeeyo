using Zeeyo.Domain.Enums;

namespace Zeeyo.Service.DTOs.Students.Attendances;

public class AttendanceForCreationDto
{
    public long StudentId { get; set; }
    public long GroupId { get; set; }
    public Status Status { get; set; }
}
