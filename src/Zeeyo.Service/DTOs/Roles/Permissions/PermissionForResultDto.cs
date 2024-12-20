using Zeeyo.Service.DTOs.Roles.RolePermissions;

namespace Zeeyo.Service.DTOs.Roles.Permissions;

public class PermissionForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ICollection<RolePermissionForResultDto> Roles { get; set; }
}
