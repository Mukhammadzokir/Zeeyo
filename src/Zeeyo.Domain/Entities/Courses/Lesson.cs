using Zeeyo.Domain.Commons;

namespace Zeeyo.Domain.Entities.Courses;

public class Lesson : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; }
    public DateTime Date { get; set; }
}
