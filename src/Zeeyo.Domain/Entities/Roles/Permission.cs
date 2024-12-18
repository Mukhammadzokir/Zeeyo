using Zeeyo.Domain.Commons;

namespace Zeeyo.Domain.Entities.Roles;

public class Permission : Auditable
{
    public string Name { get; set; }
    public ICollection<RolePermission> Roles { get; set; }
}
