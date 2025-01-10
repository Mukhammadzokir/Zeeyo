using Zeeyo.Service.Configurations;
using Zeeyo.Service.DTOs.Students.Attendances;

namespace Zeeyo.Service.Interfaces.Students;

public interface IAttendanceService
{
    Task<bool> RemoveAsync(long id);
    Task<AttendanceForResultDto> RetrieveByIdAsync(long id);
    Task<AttendanceForResultDto> AddAsync(AttendanceForCreationDto dto);
    Task<AttendanceForResultDto> ModifyAsync(long id, AttendanceForUpdateDto dto);
    Task<IEnumerable<AttendanceForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<IEnumerable<AttendanceForResultDto>> SearchAllByDateAsync(string search, PaginationParams @params);
}
