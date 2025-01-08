using System.ComponentModel.DataAnnotations;

namespace Zeeyo.Service.DTOs.Students.Enrollments;

public class EnrollmentForCreationDto
{
    [Required]
    public long StudentId { get; set; }
    [Required]
    public long GroupId { get; set; }
    [Required]
    public DateTime EnrollmentDate { get; set; }
}
