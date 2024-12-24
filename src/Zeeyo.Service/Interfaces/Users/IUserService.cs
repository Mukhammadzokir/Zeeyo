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
    Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<UserForResultDto>> SearchAllAsync(string search, PaginationParams @params);

    // ProfilePhoto
    Task<bool> RemoveProfilePhotoAsync(long userId);
    Task<UserProfilePhotoForResultDto> RetrieveProfilePhotoAsync(long userId);
    Task<UserProfilePhotoForResultDto> AddProfilePhotoAsync(long userId, IFormFile formFile);

    Task<IEnumerable<UserForResultDto>> GetAllByCourseAsync(long id);
    Task<bool> ChangePasswordAsync(long id, UserForChangePasswordDto dto);
    Task<bool> ForgetPasswordAsync(string PhoneNumber, string NewPassword, string ConfirmPassword);
    Task<UserForResultDto> RetrieveByPhoneNumberAsync(string phoneNumber);
    Task<IEnumerable<UserForResultDto>> RetrieveAllAdminsAsync(PaginationParams @params);
    Task<IEnumerable<UserForResultDto>> RetrieveAllTeachersAsync(PaginationParams @params);
    Task<IEnumerable<UserForResultDto>> SearchAdminsAsync(string search, PaginationParams @params);
    Task<IEnumerable<UserForResultDto>> SearchTeachersAsync(string search, PaginationParams @params);
}
