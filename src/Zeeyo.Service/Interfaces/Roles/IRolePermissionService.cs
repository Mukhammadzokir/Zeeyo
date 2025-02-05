using Zeeyo.Service.Configurations;
using Zeeyo.Service.DTOs.Roles.RolePermissions;

namespace Zeeyo.Service.Interfaces.Roles;

public interface IRolePermissionService
{
    Task<bool> RemoveAsync(long id);
    Task<RolePermissionForResultDto> RetrieveByIdAsync(long id);
    Task<RolePermissionForResultDto> AddAsync(RolePermissionForCreationDto dto);
    Task<RolePermissionForResultDto> ModifyAsync(long id, RolePermissionForUpdateDto dto);
    Task<IEnumerable<RolePermissionForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
