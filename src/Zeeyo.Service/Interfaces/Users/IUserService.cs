using Microsoft.AspNetCore.Http;
using Zeeyo.Service.Configurations;
using Zeeyo.Service.DTOs.Users.Users;

namespace Zeeyo.Service.Interfaces.Users;

public interface IUserService 
{
    Task<bool> RemoveAsync(long id);
    Task<UserForResultDto> RetrieveByIdAsync(long id);
    Task<UserForResultDto> AddAsync(UserForCreationDto dto);
    Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto);
    Task<UserForResultDto> RetrieveByPhoneNumberAsync(string phoneNumber);
    Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<UserForResultDto>> SearchAllAsync(string search, PaginationParams @params);
    Task<IEnumerable<UserForResultDto>> RetrieveAllByBranchIdAsync(long branchId, PaginationParams @params);

    // ProfilePhoto
    Task<bool> RemoveProfilePhotoAsync(long userId);
    Task<UserProfilePhotoForResultDto> RetrieveProfilePhotoAsync(long userId);
    Task<UserProfilePhotoForResultDto> AddProfilePhotoAsync(long userId, IFormFile formFile);

    Task<bool> ChangePasswordAsync(long id, UserForChangePasswordDto dto);
    Task<bool> CheckUserAsync(string phoneNumber);
    Task<bool> ResetPasswordAsync(string phoneNumberOrEmail, string newPassword, string confirmPassword);
}
