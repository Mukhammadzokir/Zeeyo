using Zeeyo.Service.Configurations;
using Zeeyo.Service.DTOs.Users.UserRoles;

namespace Zeeyo.Service.Interfaces.Roles;

public interface IUserRoleService
{
    Task<bool> RemoveAsync(long id);
    Task<UserRoleForResultDto> RetrieveByIdAsync(long id);
    Task<UserRoleForResultDto> AddAsync(UserRoleForCreationDto dto);
    Task<UserRoleForResultDto> ModifyAsync(long id, UserRoleForUpdateDto dto);
    Task<IEnumerable<UserRoleForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
