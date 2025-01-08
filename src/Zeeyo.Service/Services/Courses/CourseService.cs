using AutoMapper;
using Zeeyo.Domain.Extensions;
using Zeeyo.Service.Exceptions;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.Configurations;
using Microsoft.EntityFrameworkCore;
using Zeeyo.Domain.Entities.Courses;
using Zeeyo.Domain.Entities.Branches;
using Zeeyo.Service.Interfaces.Courses;
using Zeeyo.Service.DTOs.Courses.Courses;

namespace Zeeyo.Service.Services.Courses;

public class CourseService : ICourseService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<Course> _courseRepository;

    public CourseService(
        IMapper mapper,
        IRepository<Branch> branchRepository,
        IRepository<Course> courseRepository)
    {
        _mapper = mapper;
        _branchRepository = branchRepository;
        _courseRepository = courseRepository;
    }
    public async Task<CourseForResultDto> AddAsync(CourseForCreationDto dto)
    {
        var branchData = await _branchRepository.SelectAsync(b => b.Id == dto.BranchId);
        if (branchData is null)
            throw new ZeeyoException(404, "Branch is not found");

        var courseData = await _courseRepository.SelectAsync(c => c.Name.ToLower() == dto.Name.ToLower());
        if (courseData is not null)
            throw new ZeeyoException(409, "Course is already exist");

        var mappedData = _mapper.Map<Course>(dto);

        return _mapper.Map<CourseForResultDto>(await _courseRepository.InsertAsync(mappedData));
    }

    public async Task<CourseForResultDto> ModifyAsync(long id, CourseForUpdateDto dto)
    {
        var branchData = await _branchRepository.SelectAsync(b => b.Id == dto.BranchId);
        if (branchData is null)
            throw new ZeeyoException(404, "Branch is not found");

        var courseData = await _courseRepository.SelectAsync(c => c.Id == id);
        if (courseData is null)
            throw new ZeeyoException(404, "Course is not found");

        var mappedData = _mapper.Map(dto, courseData);
        mappedData.UpdatedAt = DateTime.UtcNow;

        await _courseRepository.UpdateAsync(mappedData);

        return _mapper.Map<CourseForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var courseData = await _courseRepository.SelectAsync(c => c.Id == id);
        if (courseData is null)
            throw new ZeeyoException(404, "Course is not found");

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
                    || c.Description.ToLower().Contains(search.ToLower()))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<CourseForResultDto>>(courseData);
    }
}
