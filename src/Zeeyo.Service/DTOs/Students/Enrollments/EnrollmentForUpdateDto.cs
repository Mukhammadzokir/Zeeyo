namespace Zeeyo.Service.DTOs.Students.Enrollments;

public class EnrollmentForUpdateDto
{
    public long StudentId { get; set; }
    public long GroupId { get; set; }
    public DateTime EnrollmentDate { get; set; }
}
