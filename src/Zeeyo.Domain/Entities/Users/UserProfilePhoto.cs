using Zeeyo.Domain.Entities.Assets;

namespace Zeeyo.Domain.Entities.Users;

public class UserProfilePhoto : Asset
{
    public long UserId { get; set; }
    public User User { get; set; }
}
