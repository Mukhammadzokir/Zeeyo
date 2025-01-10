using AutoMapper;
using Zeeyo.Service.Helpers;
using Zeeyo.Domain.Extensions;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.Exceptions;
using Zeeyo.Domain.Entities.Users;
using Zeeyo.Service.Configurations;
using Microsoft.EntityFrameworkCore;
using Zeeyo.Domain.Entities.Courses;
using Zeeyo.Domain.Entities.Students;
using Zeeyo.Service.Interfaces.Students;
using Zeeyo.Service.DTOs.Students.Attendances;

namespace Zeeyo.Service.Services.Students;

public class AttendanceService : IAttendanceService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Group> _groupRepository;
    private readonly IRepository<User> _studentRepository;
    private readonly IRepository<Attendance> _attendanceRepository;

    public AttendanceService(
        IMapper mapper,
        IRepository<Group> groupRepository,
        IRepository<User> studentRepository,
        IRepository<Attendance> attendanceRepository)
    {
        _mapper = mapper;
        _groupRepository = groupRepository;
        _studentRepository = studentRepository;
        _attendanceRepository = attendanceRepository;
    }

    public async Task<AttendanceForResultDto> AddAsync(AttendanceForCreationDto dto)
    {
        var groupData = await _groupRepository
            .SelectAsync(g => g.Id == dto.GroupId);
        if (groupData is null)
            throw new ZeeyoException(404, "Group is not found");

        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == dto.StudentId);
        if (studentData is null)
            throw new ZeeyoException(404, "Student is not found");

        var attendanceData = await _attendanceRepository
            .SelectAsync(a => a.StudentId == dto.StudentId && a.GroupId == dto.GroupId);
        if (attendanceData is not null)
            throw new ZeeyoException(409, "Attendance is already exist");

        var mappedData = _mapper.Map<Attendance>(dto);
        mappedData.CreatedAt = TimeHelper.GetCurrentServerTime();

        return _mapper.Map<AttendanceForResultDto>(await _attendanceRepository.InsertAsync(mappedData));
    }

    public async Task<AttendanceForResultDto> ModifyAsync(long id, AttendanceForUpdateDto dto)
    {
        var groupData = await _groupRepository
           .SelectAsync(g => g.Id == dto.GroupId);
        if (groupData is null)
            throw new ZeeyoException(404, "Group is not found");

        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == dto.StudentId);
        if (studentData is null)
            throw new ZeeyoException(404, "Student is not found");

        var attendanceData = await _attendanceRepository
            .SelectAsync(a => a.Id == id);
        if (attendanceData is null)
            throw new ZeeyoException(404, "Attendance is not found");

        var mappedData = _mapper.Map(dto, attendanceData);
        mappedData.UpdatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.UpdatedBy = HttpContextHelper.UserId;

        await _attendanceRepository.UpdateAsync(mappedData);

        return _mapper.Map<AttendanceForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var attendanceData = await _attendanceRepository
            .SelectAsync(a => a.Id == id);
        if (attendanceData is null)
            throw new ZeeyoException(404, "Attendance is not found");

        attendanceData.DeletedAt = TimeHelper.GetCurrentServerTime();
        attendanceData.DeletedBy = HttpContextHelper.UserId;

        return await _attendanceRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<AttendanceForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var attendanceData = await _attendanceRepository
             .SelectAll()
             //.Include(a => a.Student)
             //.Include(a => a.Group)
             .AsNoTracking()
             .ToPagedList(@params)
             .ToListAsync();

        return _mapper.Map<IEnumerable<AttendanceForResultDto>>(attendanceData);
    }

    public async Task<AttendanceForResultDto> RetrieveByIdAsync(long id)
    {
        var attendanceData = await _attendanceRepository
            .SelectAll(a => a.Id == id)
            //.Include(a => a.Student)
            //.Include(a => a.Group)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (attendanceData is null)
            throw new ZeeyoException(404, "Attendance is not found");

        return _mapper.Map<AttendanceForResultDto>(attendanceData);
    }

    public async Task<IEnumerable<AttendanceForResultDto>> SearchAllByDateAsync(string search, PaginationParams @params)
    {
        var attendanceData = await _attendanceRepository
            .SelectAll()
            .Where(a => a.Date.ToString("dd/MM/yyyy") == search)
            .Include(a => a.Student)
            .Include(a => a.Group)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<AttendanceForResultDto>>(attendanceData);
    }
}
