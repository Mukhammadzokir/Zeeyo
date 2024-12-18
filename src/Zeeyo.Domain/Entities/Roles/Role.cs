using Zeeyo.Domain.Commons;
using Zeeyo.Domain.Entities.Users;

namespace Zeeyo.Domain.Entities.Roles;

public class Role : Auditable
{
    public string Name { get; set; }
    public ICollection<UserRole> Users { get; set; }
    public ICollection<RolePermission> Permissions { get; set; }
}
