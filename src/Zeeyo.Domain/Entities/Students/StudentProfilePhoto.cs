using Zeeyo.Domain.Entities.Assets;

namespace Zeeyo.Domain.Entities.Students;

public class StudentProfilePhoto : Asset
{
    public long StudentId { get; set; }
    public Student Student { get; set; }
}
