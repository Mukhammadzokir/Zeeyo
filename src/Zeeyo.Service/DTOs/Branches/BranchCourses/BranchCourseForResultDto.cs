using Zeeyo.Domain.Entities.Courses;
using Zeeyo.Domain.Entities.Branches;

namespace Zeeyo.Service.DTOs.Branches.BranchCourses;

public class BranchCourseForResultDto
{
    public long Id { get; set; }
    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
}
