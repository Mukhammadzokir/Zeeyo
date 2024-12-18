using Zeeyo.Domain.Commons;
using Zeeyo.Domain.Entities.Roles;

namespace Zeeyo.Domain.Entities.Users;

public class UserRole : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long RoleId { get; set; }
    public Role Role { get; set; }
}
