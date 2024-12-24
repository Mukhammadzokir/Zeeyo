using Microsoft.AspNetCore.Http;
using Zeeyo.Service.Configurations;
using Zeeyo.Service.DTOs.Users.Users;
using Zeeyo.Service.Interfaces.Users;

namespace Zeeyo.Service.Services.Users;

public class UserService : IUserService
{

    public Task<UserForResultDto> AddAsync(UserForCreationDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<UserProfilePhotoForResultDto> AddProfilePhotoAsync(long userId, IFormFile formFile)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangePasswordAsync(long id, UserForChangePasswordDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ForgetPasswordAsync(string PhoneNumber, string NewPassword, string ConfirmPassword)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserForResultDto>> GetAllByCourseAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveProfilePhotoAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserForResultDto>> RetrieveAllAdminsAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserForResultDto>> RetrieveAllTeachersAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<UserForResultDto> RetrieveByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<UserForResultDto> RetrieveByPhoneNumberAsync(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    public Task<UserProfilePhotoForResultDto> RetrieveProfilePhotoAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserForResultDto>> SearchAdminsAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserForResultDto>> SearchTeachersAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }
}
