using Microsoft.AspNetCore.Http;
using Zeeyo.Service.Configurations;
using Zeeyo.Service.DTOs.Students.Students;
using Zeeyo.Service.DTOs.Users.Users;

namespace Zeeyo.Service.Interfaces.Students;

public interface IStudentService
{
    Task<bool> RemoveAsync(long id);
    Task<StudentForResultDto> RetrieveByIdAsync(long id);
    Task<StudentForResultDto> AddAsync(StudentForCreationDto dto);
    Task<StudentForResultDto> ModifyAsync(long id, StudentForUpdateDto dto);
    Task<StudentForResultDto> RetrieveByPhoneNumberAsync(string phoneNumber);
    Task<IEnumerable<StudentForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<StudentForResultDto>> SearchAllAsync(string search, PaginationParams @params);
    Task<IEnumerable<StudentForResultDto>> RetrieveAllByBranchIdAsync(long branchId, PaginationParams @params);

    // ProfilePhoto
    Task<bool> RemoveProfilePhotoAsync(long studentId);
    Task<StudentProfilePhotoForResultDto> RetrieveProfilePhotoAsync(long studentId);
    Task<StudentProfilePhotoForResultDto> AddProfilePhotoAsync(long studentId, IFormFile formFile);
}
