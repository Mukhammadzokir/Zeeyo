using Zeeyo.Service.Configurations;
using Zeeyo.Service.DTOs.Courses.Courses;

namespace Zeeyo.Service.Interfaces.Courses;

public interface ICourseService
{
    Task<bool> RemoveAsync(long id);
    Task<CourseForResultDto> RetrieveByIdAsync(long id);
    Task<CourseForResultDto> AddAsync(CourseForCreationDto dto);
    Task<CourseForResultDto> ModifyAsync(long id, CourseForUpdateDto dto);
    Task<IEnumerable<CourseForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<CourseForResultDto>> SearchAllAsync(string search, PaginationParams @params);
}
