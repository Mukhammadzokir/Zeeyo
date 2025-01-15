using Zeeyo.Service.DTOs.Users.UserCodes;
using Zeeyo.Service.DTOs.Users.UserRoles;
using Zeeyo.Service.DTOs.Students.Students;
using Zeeyo.Service.DTOs.Teachers.Teachers;

namespace Zeeyo.Service.DTOs.Users.Users;

public class UserForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public long BranchId { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    //public bool IsVerified { get; set; } = false;
    //public string RefreshToken { get; set; }
    //public DateTime ExpireDate { get; set; }
    public ICollection<UserRoleForResultDto> UserRoles { get; set; }
    public ICollection<UserCodeForResultDto> UserCodes { get; set; }
}
