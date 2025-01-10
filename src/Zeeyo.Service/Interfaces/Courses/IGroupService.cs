using Zeeyo.Service.Configurations;
using Zeeyo.Service.DTOs.Courses.Groups;

namespace Zeeyo.Service.Interfaces.Courses;

public interface IGroupService
{
    Task<bool> RemoveAsync(long id);
    Task<GroupForResultDto> RetrieveByIdAsync(long id);
    Task<GroupForResultDto> AddAsync(GroupForCreationDto dto);
    Task<GroupForResultDto> ModifyAsync(long id, GroupForUpdateDto dto);
    Task<IEnumerable<GroupForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<GroupForResultDto>> SearchAllAsync(string search, PaginationParams @params);
}
