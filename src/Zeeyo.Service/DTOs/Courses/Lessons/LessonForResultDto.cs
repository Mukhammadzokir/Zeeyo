using Zeeyo.Domain.Entities.Courses;

namespace Zeeyo.Service.DTOs.Courses.Lessons;

public class LessonForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public long GroupId { get; set; }
    public Group Group { get; set; }
    public DateTime Date { get; set; }
}
