using Zeeyo.Domain.Commons;
using Zeeyo.Domain.Entities.Courses;

namespace Zeeyo.Domain.Entities.Branches;

public class BranchCourse : Auditable
{
    public long BranchId { get; set; }
    public Branch Branch { get; set; }
    public long CourseId { get; set; }
    public Course Course { get; set; }
}
