using Zeeyo.Domain.Commons;
using Zeeyo.Domain.Entities.Students;
using Zeeyo.Domain.Entities.Teachers;

namespace Zeeyo.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long BranchId { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsVerified { get; set; } = false;
    public string RefreshToken { get; set; }
    public DateTime ExpireDate { get; set; }
    public Student Student { get; set; }
    public Teacher Teacher { get; set; }
    public ICollection<UserRole> Roles { get; set; }
    public ICollection<UserCode> UserCodes { get; set; }
}
