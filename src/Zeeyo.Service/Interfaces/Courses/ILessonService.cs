using Zeeyo.Service.Configurations;
using Zeeyo.Service.DTOs.Courses.Lessons;

namespace Zeeyo.Service.Interfaces.Courses;

public interface ILessonService
{
    Task<bool> RemoveAsync(long id);
    Task<LessonForResultDto> RetrieveByIdAsync(long id);
    Task<LessonForResultDto> AddAsync(LessonForCreationDto dto);
    Task<LessonForResultDto> ModifyAsync(long id, LessonForUpdateDto dto);
    Task<IEnumerable<LessonForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<LessonForResultDto>> SearchAllAsync(string search, PaginationParams @params);
}
