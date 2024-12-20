using Zeeyo.Service.DTOs.Users.UserRoles;
using Zeeyo.Service.DTOs.Roles.RolePermissions;

namespace Zeeyo.Service.DTOs.Roles.Roles;

public class RoleForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<UserRoleForResultDto> Users { get; set; }
    public ICollection<RolePermissionForResultDto> Permissions { get; set; }
}
