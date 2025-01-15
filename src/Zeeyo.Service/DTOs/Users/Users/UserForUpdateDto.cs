using Zeeyo.Service.Commons.Attributes;

namespace Zeeyo.Service.DTOs.Users.Users;

public class UserForUpdateDto
{
    public long BranchId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [PhoneNumberAttribute]
    public string PhoneNumber { get; set; }

    [CustomEmailAddressAttribute]
    public string Email { get; set; }
}
