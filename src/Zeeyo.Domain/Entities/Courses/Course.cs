using Zeeyo.Domain.Commons;
using Zeeyo.Domain.Entities.Branches;

namespace Zeeyo.Domain.Entities.Courses;

public class Course : Auditable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public ICollection<Group> Groups { get; set; }
    public ICollection<BranchCourse> Branches { get; set; }

}
