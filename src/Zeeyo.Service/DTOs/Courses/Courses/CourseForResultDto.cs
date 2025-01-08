using Zeeyo.Domain.Entities.Branches;
using Zeeyo.Service.DTOs.Branches;
using Zeeyo.Service.DTOs.Branches.BranchCourses;
using Zeeyo.Service.DTOs.Courses.Groups;
using Zeeyo.Service.DTOs.Courses.Lessons;
using Zeeyo.Service.DTOs.Students.Attendances;
using Zeeyo.Service.DTOs.Students.Enrollments;
using Zeeyo.Service.DTOs.Teachers.TeacherCourses;

namespace Zeeyo.Service.DTOs.Courses.Courses;

public class CourseForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ICollection<GroupForResultDto> Groups { get; set; }
    public ICollection<BranchCourseForResultDto> Branches { get; set; }
}
