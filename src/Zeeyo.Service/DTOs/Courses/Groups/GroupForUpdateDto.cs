namespace Zeeyo.Service.DTOs.Courses.Groups;

public class GroupForUpdateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public long CourseId { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateTime EndDate { get; set; }
}
