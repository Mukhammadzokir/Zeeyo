using Zeeyo.Domain.Commons;

namespace Zeeyo.Domain.Entities.Users;

public class UserRole : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long RoleId { get; set; }
    public Role Role { get; set; }
}
