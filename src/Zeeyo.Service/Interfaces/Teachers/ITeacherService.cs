using Microsoft.AspNetCore.Http;
using Zeeyo.Service.Configurations;
using Zeeyo.Service.DTOs.Users.Users;
using Zeeyo.Service.DTOs.Teachers.Teachers;

namespace Zeeyo.Service.Interfaces.Teachers;

public interface ITeacherService
{
    Task<bool> RemoveAsync(long id);
    Task<TeacherForResultDto> RetrieveByIdAsync(long id);
    Task<TeacherForResultDto> AddAsync(TeacherForCreationDto dto);
    Task<TeacherForResultDto> ModifyAsync(long id, TeacherForUpdateDto dto);
    Task<TeacherForResultDto> RetrieveByPhoneNumberAsync(string phoneNumber);
    Task<IEnumerable<TeacherForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<TeacherForResultDto>> SearchAllAsync(string search, PaginationParams @params);
    Task<IEnumerable<TeacherForResultDto>> RetrieveAllByBranchIdAsync(long branchId, PaginationParams @params);

    // ProfilePhoto
    Task<bool> RemoveProfilePhotoAsync(long teacherId);
    Task<TeacherProfilePhotoForResultDto> RetrieveProfilePhotoAsync(long teacherId);
    Task<TeacherProfilePhotoForResultDto> AddProfilePhotoAsync(long teacherId, IFormFile formFile);
    
    Task<bool> ChangePasswordAsync(long id, UserForChangePasswordDto dto);
    Task<bool> ForgetPasswordAsync(string PhoneNumber, string NewPassword, string ConfirmPassword);
}
