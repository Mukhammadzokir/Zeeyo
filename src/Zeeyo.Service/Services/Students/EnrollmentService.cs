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
using Zeeyo.Service.DTOs.Students.Enrollments;

namespace Zeeyo.Service.Services.Students;

public class EnrollmentService : IEnrollmentService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Group> _groupRepository;
    private readonly IRepository<User> _studentRepository;
    private readonly IRepository<Enrollment> _enrollmentRepository;

    public EnrollmentService(
        IMapper mapper,
        IRepository<Group> groupRepository,
        IRepository<User> studentRepository,
        IRepository<Enrollment> enrollmentRepository)
    {
        _mapper = mapper;
        _groupRepository = groupRepository;
        _studentRepository = studentRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<EnrollmentForResultDto> AddAsync(EnrollmentForCreationDto dto)
    {
        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == dto.StudentId);
        if (studentData is null)
            throw new ZeeyoException(404, "Student is not found");

        var groupData = await _groupRepository
            .SelectAsync(g => g.Id == dto.GroupId);
        if (groupData is null)
            throw new ZeeyoException(404, "Group is not found");

        var enrollmentData = await _enrollmentRepository
            .SelectAsync(e => e.StudentId == dto.StudentId && e.GroupId == dto.GroupId);
        if (enrollmentData is not null)
            throw new ZeeyoException(409, "Enrollment is already exist");

        var mappedData = _mapper.Map<Enrollment>(dto);
        mappedData.CreatedAt = TimeHelper.GetCurrentServerTime();

        return _mapper.Map<EnrollmentForResultDto>(await _enrollmentRepository.InsertAsync(mappedData));
    }

    public async Task<EnrollmentForResultDto> ModifyAsync(long id, EnrollmentForUpdateDto dto)
    {
        var studentData = await _studentRepository
             .SelectAsync(s => s.Id == dto.StudentId);
        if (studentData is null)
            throw new ZeeyoException(404, "Student is not found");

        var groupData = await _groupRepository
            .SelectAsync(g => g.Id == dto.GroupId);
        if (groupData is null)
            throw new ZeeyoException(404, "Group is not found");

        var enrollmentData = await _enrollmentRepository
            .SelectAsync(e => e.Id == id);
        if (enrollmentData is not null)
            throw new ZeeyoException(404, "Enrollment is not found");

        var mappedData = _mapper.Map(dto, enrollmentData);
        mappedData.UpdatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.UpdatedBy = HttpContextHelper.UserId;

        await _enrollmentRepository.UpdateAsync(mappedData);

        return _mapper.Map<EnrollmentForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var enrollmentData = await _enrollmentRepository
            .SelectAsync(e => e.Id == id);
        if (enrollmentData is null)
            throw new ZeeyoException(404, "Enrollment is not found");

        enrollmentData.DeletedAt = TimeHelper.GetCurrentServerTime();
        enrollmentData.DeletedBy = HttpContextHelper.UserId;

        return await _enrollmentRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<EnrollmentForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var enrollmentData = await _enrollmentRepository
            .SelectAll()
            //.Include(e => e.Student)
            //.Include(e => e.Group)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<EnrollmentForResultDto>>(enrollmentData);
    }

    public async Task<EnrollmentForResultDto> RetrieveByIdAsync(long id)
    {
        var enrollmentData = await _enrollmentRepository
            .SelectAsync(e => e.Id == id);
        if (enrollmentData is null)
            throw new ZeeyoException(404, "Enrollment is not found");

        return _mapper.Map<EnrollmentForResultDto>(enrollmentData);
    }

    public async Task<IEnumerable<EnrollmentForResultDto>> SearchAllByDateAsync(string search, PaginationParams @params)
    {
        var enrollmentData = await _enrollmentRepository
            .SelectAll()
            .Where(e => e.EnrollmentDate.ToString("dd/MM/yyyy") == search)
            .Include(e => e.Student)
            .Include(e => e.Group)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<EnrollmentForResultDto>>(enrollmentData);
    }
}
