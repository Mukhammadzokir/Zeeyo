using AutoMapper;
using Zeeyo.Domain.Extensions;
using Zeeyo.Service.Exceptions;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.Configurations;
using Microsoft.EntityFrameworkCore;
using Zeeyo.Domain.Entities.Courses;
using Zeeyo.Service.Interfaces.Courses;
using Zeeyo.Service.DTOs.Courses.Courses;
using Zeeyo.Service.Helpers;

namespace Zeeyo.Service.Services.Courses;

public class CourseService : ICourseService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Course> _courseRepository;

    public CourseService(
        IMapper mapper,
        IRepository<Course> courseRepository)
    {
        _mapper = mapper;
        _courseRepository = courseRepository;
    }
    public async Task<CourseForResultDto> AddAsync(CourseForCreationDto dto)
    {
        var courseData = await _courseRepository.SelectAsync(c => c.Name.ToLower() == dto.Name.ToLower());
        if (courseData is not null)
            throw new ZeeyoException(409, "Course is already exist");

        var mappedData = _mapper.Map<Course>(dto);
        mappedData.CreatedAt = TimeHelper.GetCurrentServerTime();

        return _mapper.Map<CourseForResultDto>(await _courseRepository.InsertAsync(mappedData));
    }

    public async Task<CourseForResultDto> ModifyAsync(long id, CourseForUpdateDto dto)
    {
        var courseData = await _courseRepository.SelectAsync(c => c.Id == id);
        if (courseData is null)
            throw new ZeeyoException(404, "Course is not found");

        var mappedData = _mapper.Map(dto, courseData);
        mappedData.DeletedBy = HttpContextHelper.UserId;
        mappedData.UpdatedAt = TimeHelper.GetCurrentServerTime();

        await _courseRepository.UpdateAsync(mappedData);

        return _mapper.Map<CourseForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var courseData = await _courseRepository.SelectAsync(c => c.Id == id);
        if (courseData is null)
            throw new ZeeyoException(404, "Course is not found");

        courseData.DeletedAt = TimeHelper.GetCurrentServerTime();
        courseData.DeletedBy = HttpContextHelper.UserId;

        return await _courseRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<CourseForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var courseData = await _courseRepository
            .SelectAll()
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CourseForResultDto>>(courseData);
    }

    public async Task<CourseForResultDto> RetrieveByIdAsync(long id)
    {
        var courseData = await _courseRepository.SelectAsync(c => c.Id == id);
        if (courseData is null)
            throw new ZeeyoException(404, "Course is not found");

        return _mapper.Map<CourseForResultDto>(courseData);
    }

    public async Task<IEnumerable<CourseForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var courseData = await _courseRepository
            .SelectAll()
            .Where(c => c.Name.ToLower().Contains(search.ToLower())
                    || c.Price.Equals(search))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CourseForResultDto>>(courseData);
    }
}