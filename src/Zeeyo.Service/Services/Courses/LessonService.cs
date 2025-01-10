using AutoMapper;
using Zeeyo.Service.Helpers;
using Zeeyo.Domain.Extensions;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.Exceptions;
using Zeeyo.Service.Configurations;
using Microsoft.EntityFrameworkCore;
using Zeeyo.Domain.Entities.Courses;
using Zeeyo.Service.Interfaces.Courses;
using Zeeyo.Service.DTOs.Courses.Lessons;

namespace Zeeyo.Service.Services.Courses;

public class LessonService : ILessonService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Group> _groupRepository;
    private readonly IRepository<Lesson> _lessonRepository;
    public LessonService(
        IMapper mapper,
        IRepository<Group> groupRepository,
        IRepository<Lesson> lessonRepository)
    {
        _mapper = mapper;
        _groupRepository = groupRepository;
        _lessonRepository = lessonRepository;
    }

    public async Task<LessonForResultDto> AddAsync(LessonForCreationDto dto)
    {
        var groupData = await _groupRepository
            .SelectAsync(c => c.Id == dto.GroupId);
        if (groupData is null)
            throw new ZeeyoException(404, "Group is not found");

        var lessonData = await _groupRepository
            .SelectAsync(l => l.Name.ToLower() == dto.Name.ToLower());
        if (lessonData is not null)
            throw new ZeeyoException(404, "Lesson is already exist");

        var mappedData = _mapper.Map<Lesson>(dto);
        mappedData.CreatedAt = TimeHelper.GetCurrentServerTime();

        return _mapper.Map<LessonForResultDto>(await _lessonRepository.InsertAsync(mappedData));
    }

    public async Task<LessonForResultDto> ModifyAsync(long id, LessonForUpdateDto dto)
    {
        var groupData = await _groupRepository
            .SelectAsync(c => c.Id == dto.GroupId);
        if (groupData is null)
            throw new ZeeyoException(404, "Group is not found");

        var lessonData = await _lessonRepository
            .SelectAsync(c => c.Id == id);
        if (lessonData is null)
            throw new ZeeyoException(404, "Lesson is not found");

        var mappedData = _mapper.Map(dto, lessonData);
        mappedData.UpdatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.UpdatedBy = HttpContextHelper.UserId;

        await _lessonRepository.UpdateAsync(mappedData);

        return _mapper.Map<LessonForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var lessonData = await _lessonRepository
            .SelectAsync(l => l.Id == id);
        if (lessonData is null)
            throw new ZeeyoException(404, "Lesson is not found");

        lessonData.DeletedAt = TimeHelper.GetCurrentServerTime();
        lessonData.DeletedBy = HttpContextHelper.UserId;

        return await _lessonRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<LessonForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var lessonData = await _lessonRepository
            .SelectAll()
            //.Include(l => l.Group)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<LessonForResultDto>>(lessonData);
    }

    public async Task<LessonForResultDto> RetrieveByIdAsync(long id)
    {
        var lessonData = await _lessonRepository
            .SelectAll()
            //.Where(l => l.Id == id && l.Group.IsDeleted == false)
            //.Include(c => c.Group)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (lessonData is null)
            throw new ZeeyoException(404, "Lesson is not found");

        return _mapper.Map<LessonForResultDto>(lessonData);
    }

    public async Task<IEnumerable<LessonForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var lessonData = await _lessonRepository
           .SelectAll()
           .Where(l => l.Name.ToLower().Contains(search.ToLower())
               || l.Description.ToLower().Contains(search.ToLower())
               || l.Date.ToString().Contains(search))
           .AsNoTracking()
           .ToPagedList(@params)
           .ToListAsync();

        return _mapper.Map<IEnumerable<LessonForResultDto>>(lessonData);
    }
}
