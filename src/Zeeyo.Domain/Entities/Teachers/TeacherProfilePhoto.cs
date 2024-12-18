using Zeeyo.Domain.Entities.Assets;

namespace Zeeyo.Domain.Entities.Teachers;

public class TeacherProfilePhoto : Asset
{
    public long TeacherId { get; set; }
    public Teacher Teacher { get; set; }
}
