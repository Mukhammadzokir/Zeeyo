using AutoMapper;
using Zeeyo.Service.Helpers;
using Zeeyo.Domain.Extensions;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.Exceptions;
using Zeeyo.Domain.Entities.Users;
using Zeeyo.Service.Configurations;
using Microsoft.EntityFrameworkCore;
using Zeeyo.Domain.Entities.Courses;
using Zeeyo.Domain.Entities.Teachers;
using Zeeyo.Service.Interfaces.Teachers;
using Zeeyo.Service.DTOs.Teachers.TeacherGroups;

namespace Zeeyo.Service.Services.Teachers;

public class TeacherGroupService : ITeacherGroupService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Group> _groupRepository;
    private readonly IRepository<User> _teacherRepository;
    private readonly IRepository<TeacherGroup> _teacherGroupRepository;

    public TeacherGroupService(
        IMapper mapper,
        IRepository<Group> groupRepository,
        IRepository<User> teacherRepository,
        IRepository<TeacherGroup> teacherGroupRepository)
    {
        _mapper = mapper;
        _groupRepository = groupRepository;
        _teacherRepository = teacherRepository;
        _teacherGroupRepository = teacherGroupRepository;
    }

    public async Task<TeacherGroupForResultDto> AddAsync(TeacherGroupForCreationDto dto)
    {
        var teacherData = await _teacherRepository
            .SelectAsync(t => t.Id == dto.TeacherId);
        if (teacherData is null)
            throw new ZeeyoException(404, "Teacher is not found");

        var groupData = await _groupRepository
            .SelectAsync(g => g.Id == dto.GroupId);
        if (groupData is null)
            throw new ZeeyoException(404, "Group is not found");

        var teacherGroupData = await _teacherGroupRepository
            .SelectAsync(tg => tg.TeacherId == dto.TeacherId && tg.GroupId == dto.GroupId);
        if (teacherGroupData is not null)
            throw new ZeeyoException(409, "TeacherGroup is already exist");

        var mappedData = _mapper.Map<TeacherGroup>(dto);
        mappedData.CreatedAt = TimeHelper.GetCurrentServerTime();

        return _mapper.Map<TeacherGroupForResultDto>(await _teacherGroupRepository.InsertAsync(mappedData));
    }

    public async Task<TeacherGroupForResultDto> ModifyAsync(long id, TeacherGroupForUpdateDto dto)
    {
        var teacherData = await _teacherRepository
             .SelectAsync(t => t.Id == dto.TeacherId);
        if (teacherData is null)
            throw new ZeeyoException(404, "Teacher is not found");

        var groupData = await _groupRepository
            .SelectAsync(g => g.Id == dto.GroupId);
        if (groupData is null)
            throw new ZeeyoException(404, "Group is not found");

        var teacherGroupData = await _teacherGroupRepository
            .SelectAsync(tg => tg.Id == id);
        if (teacherGroupData is not null)
            throw new ZeeyoException(404, "TeacherGroup is not found");

        var mappedData = _mapper.Map(dto, teacherGroupData);
        mappedData.UpdatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.UpdatedBy = HttpContextHelper.UserId;

        await _teacherGroupRepository.UpdateAsync(mappedData);

        return _mapper.Map<TeacherGroupForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var teacherGroupData = await _teacherGroupRepository
            .SelectAsync(tg => tg.Id == id);
        if (teacherGroupData is null)
            throw new ZeeyoException(404, "TeacherGroup is not found");

        teacherGroupData.DeletedAt = TimeHelper.GetCurrentServerTime();
        teacherGroupData.DeletedBy = HttpContextHelper.UserId;

        return await _teacherGroupRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<TeacherGroupForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var teacherGroupData = await _teacherGroupRepository
            .SelectAll()
            //.Include(tg => tg.Teacher)
            //.Include(tg => tg.Group)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<TeacherGroupForResultDto>>(teacherGroupData);
    }

    public async Task<TeacherGroupForResultDto> RetrieveByIdAsync(long id)
    {
        var teacherGroupData = await _teacherGroupRepository
            .SelectAll()
            //.Include(tg => tg.Teacher)
            //.Include(tg => tg.Course)
            .AsNoTracking()
            .ToListAsync();

        return _mapper.Map<TeacherGroupForResultDto>(teacherGroupData);
    }
}
