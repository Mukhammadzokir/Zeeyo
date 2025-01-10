using AutoMapper;
using Zeeyo.Service.Helpers;
using Zeeyo.Domain.Extensions;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.Exceptions;
using Zeeyo.Service.Configurations;
using Microsoft.EntityFrameworkCore;
using Zeeyo.Domain.Entities.Courses;
using Zeeyo.Service.Interfaces.Courses;
using Zeeyo.Service.DTOs.Courses.Groups;

namespace Zeeyo.Service.Services.Courses;

public class GroupService : IGroupService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Group> _groupRepository;
    private readonly IRepository<Course> _courseRepository;

    public GroupService(
        IMapper mapper,
        IRepository<Group> groupRepository,
        IRepository<Course> courseRepository)
    {
        _mapper = mapper;
        _groupRepository = groupRepository;
        _courseRepository = courseRepository;
    }

    public async Task<GroupForResultDto> AddAsync(GroupForCreationDto dto)
    {

        var courseData = await _courseRepository
            .SelectAsync(c => c.Id == dto.CourseId);
        if (courseData is not null)
            throw new ZeeyoException(409, "Course is not found");

        var groupData = await _groupRepository
           .SelectAsync(g => g.Name.ToLower() == dto.Name.ToLower() );
        if (groupData is null)
            throw new ZeeyoException(404, "Group is already exist");

        var mappedData = _mapper.Map<Course>(dto);
        mappedData.CreatedAt = TimeHelper.GetCurrentServerTime();

        return _mapper.Map<GroupForResultDto>(await _courseRepository.InsertAsync(mappedData));
    }

    public async Task<GroupForResultDto> ModifyAsync(long id, GroupForUpdateDto dto)
    {
        var courseData = await _groupRepository
            .SelectAsync(g => g.Id == dto.CourseId);
        if (courseData is null)
            throw new ZeeyoException(404, "Course is not found");

        var groupData = await _groupRepository
            .SelectAsync(g => g.Id == id);
        if (groupData is null)
            throw new ZeeyoException(404, "Group is not found");

        var mappedData = _mapper.Map(dto, groupData);
        mappedData.UpdatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.UpdatedBy = HttpContextHelper.UserId;

        await _groupRepository.UpdateAsync(mappedData);

        return _mapper.Map<GroupForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var groupData = await _groupRepository
            .SelectAsync(g => g.Id == id);
        if (groupData is null)
            throw new ZeeyoException(404, "Group is not found");

        groupData.DeletedAt = TimeHelper.GetCurrentServerTime();
        groupData.DeletedBy = HttpContextHelper.UserId;

        return await _courseRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<GroupForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var groupData = await _groupRepository
            .SelectAll()
            //.Include(g => g.Teachers.Where(t => !t.IsDeleted))
            //.Include(g => g.Students.Where(s => !s.IsDeleted))
            //.Include(g => g.Lessons.Where(l => !l.IsDeleted))
            //.Include(g => g.Attendances.Where(a => !a.IsDeleted))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<GroupForResultDto>>(groupData);
    }

    public async Task<GroupForResultDto> RetrieveByIdAsync(long id)
    {
        var groupData = await _groupRepository
            .SelectAll()
            .Where(g => g.Id == id)
            //.Include(g => g.Teachers.Where(t => !t.IsDeleted))
            //.Include(g => g.Students.Where(s => !s.IsDeleted))
            //.Include(g => g.Lessons.Where(l => !l.IsDeleted))
            //.Include(g => g.Attendances.Where(a => !a.IsDeleted))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (groupData is null)
            throw new ZeeyoException(404, "Group is not found");

        return _mapper.Map<GroupForResultDto>(groupData);
    }

    public async Task<IEnumerable<GroupForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var groupData = await _groupRepository
            .SelectAll()
            .Where(g => g.Name.ToLower().Contains(search.ToLower())
                || g.Description.ToLower().Contains(search.ToLower()))
            //.Include(g => g.Teachers.Where(t => !t.IsDeleted))
            //.Include(g => g.Students.Where(s => !s.IsDeleted))
            //.Include(g => g.Lessons.Where(l => !l.IsDeleted))
            //.Include(g => g.Attendances.Where(a => !a.IsDeleted))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<GroupForResultDto>>(groupData);
    }
}
