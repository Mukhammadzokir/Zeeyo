using Zeeyo.Domain.Commons;
using Zeeyo.Domain.Entities.Users;

namespace Zeeyo.Domain.Entities.Branches;

public class Branch : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public ICollection<User> Users { get; set; }
    public ICollection<BranchCourse> Courses { get; set; }
}
