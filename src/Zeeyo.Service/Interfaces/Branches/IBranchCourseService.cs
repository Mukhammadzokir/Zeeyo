using Zeeyo.Service.Configurations;
using Zeeyo.Service.DTOs.Branches.BranchCourses;

namespace Zeeyo.Service.Interfaces.Branches;

public interface IBranchCourseService
{
    Task<bool> RemoveAsync(long id);
    Task<BranchCourseForResultDto> RetrieveByIdAsync(long id);
    Task<BranchCourseForResultDto> AddAsync(BranchCourseForCreationDto dto);
    Task<BranchCourseForResultDto> ModifyAsync(long id, BranchCourseForUpdateDto dto);
    Task<IEnumerable<BranchCourseForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
