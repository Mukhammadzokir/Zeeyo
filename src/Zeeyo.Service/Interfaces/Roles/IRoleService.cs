using Zeeyo.Service.Configurations;
using Zeeyo.Service.DTOs.Roles.Roles;
using EduNet.Backend.Service.DTOs.Roles.Roles;

namespace Zeeyo.Service.Interfaces.Roles;

public interface IRoleService
{
    Task<bool> RemoveAsync(long id);
    Task<RoleForResultDto> RetrieveByIdAsync(long id);
    Task<RoleForResultDto> AddAsync(RoleForCreationDto dto);
    Task<RoleForResultDto> ModifyAsync(long id, RoleForUpdateDto dto);
    Task<IEnumerable<RoleForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<RoleForResultDto>> SearchAllAsync(string search, PaginationParams @params);
}
