using Zeeyo.Service.Configurations;
using Zeeyo.Service.DTOs.Teachers.TeacherGroups;

namespace Zeeyo.Service.Interfaces.Teachers;

public interface ITeacherGroupService
{
    Task<bool> RemoveAsync(long id);
    Task<TeacherGroupForResultDto> RetrieveByIdAsync(long id);
    Task<TeacherGroupForResultDto> AddAsync(TeacherGroupForCreationDto dto);
    Task<TeacherGroupForResultDto> ModifyAsync(long id, TeacherGroupForUpdateDto dto);
    Task<IEnumerable<TeacherGroupForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
