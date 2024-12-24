namespace Zeeyo.Service.DTOs.Students.Students;

public class StudentProfilePhotoForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public string Extension { get; set; }
    public long Size { get; set; }
    public string Type { get; set; }
}
