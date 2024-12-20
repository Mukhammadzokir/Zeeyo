using Zeeyo.Domain.Entities.Roles;
using Zeeyo.Domain.Entities.Users;

namespace Zeeyo.Service.DTOs.Users.UserRoles;

public class UserRoleForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
    public long RoleId { get; set; }
    public Role Role { get; set; }
}
