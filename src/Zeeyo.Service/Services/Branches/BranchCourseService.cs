using AutoMapper;
using Zeeyo.Service.Helpers;
using Zeeyo.Domain.Extensions;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.Exceptions;
using Zeeyo.Service.Configurations;
using Microsoft.EntityFrameworkCore;
using Zeeyo.Domain.Entities.Courses;
using Zeeyo.Domain.Entities.Branches;
using Zeeyo.Service.Interfaces.Branches;
using Zeeyo.Service.DTOs.Branches.BranchCourses;

namespace Zeeyo.Service.Services.Branches;

public class BranchCourseService : IBranchCourseService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<Course> _courseRepository;
    private readonly IRepository<BranchCourse> _branchCourseRepository;

    public BranchCourseService(
        IMapper mapper,
        IRepository<Branch> branchRepository,
        IRepository<Course> courseRepository,
        IRepository<BranchCourse> branchCourseRepository)
    {
        _mapper = mapper;
        _branchRepository = branchRepository; 
        _courseRepository = courseRepository;
        _branchCourseRepository = branchCourseRepository;
    }

    public async Task<BranchCourseForResultDto> AddAsync(BranchCourseForCreationDto dto)
    {
        var branchData = await _branchRepository
            .SelectAsync(b => b.Id == dto.BranchId);
        if (branchData is null)
            throw new ZeeyoException(404, "Branch is not found");

        var courseData = await _courseRepository
            .SelectAsync(c => c.Id == dto.CourseId);
        if (courseData is null)
            throw new ZeeyoException(404, "Course is not found");

        var branchCourseData = await _branchCourseRepository
            .SelectAsync(bc => bc.BranchId == dto.BranchId && bc.CourseId == dto.CourseId);
        if (branchCourseData is not null)
            throw new ZeeyoException(409, "BranchCourse is already exist");

        var mappedData = _mapper.Map<BranchCourse>(dto);
        mappedData.CreatedAt = TimeHelper.GetCurrentServerTime();

        return _mapper.Map<BranchCourseForResultDto>(await _branchCourseRepository.InsertAsync(mappedData));
    }

    public async Task<BranchCourseForResultDto> ModifyAsync(long id, BranchCourseForUpdateDto dto)
    {
        var branchData = await _branchRepository
            .SelectAsync(b => b.Id == dto.BranchId);
        if (branchData is null)
            throw new ZeeyoException(404, "Branch is not found");

        var courseData = await _courseRepository
            .SelectAsync(c => c.Id == dto.CourseId);
        if (courseData is null)
            throw new ZeeyoException(404, "Course is not found");

        var branchCourseData = await _branchCourseRepository
            .SelectAsync(bc => bc.BranchId == dto.BranchId && bc.CourseId == dto.CourseId);
        if (branchCourseData is not null)
            throw new ZeeyoException(409, "BranchCourse is already exist");

        var mappedData = _mapper.Map(dto, branchCourseData);
        mappedData.UpdatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.UpdatedBy = HttpContextHelper.UserId;

        await _branchCourseRepository.UpdateAsync(mappedData);

        return _mapper.Map<BranchCourseForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var branchCourseData = await _branchCourseRepository
            .SelectAsync(bc => bc.Id == id);
        if (branchCourseData is null)
            throw new ZeeyoException(404, "BranchCourse is not found");

        branchCourseData.DeletedAt = TimeHelper.GetCurrentServerTime();
        branchCourseData.DeletedBy = HttpContextHelper.UserId;

        return await _branchCourseRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<BranchCourseForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var branchCourseData = await _branchCourseRepository
            .SelectAll()
            //.Include(bc => bc.Branch)
            //.Include(bc => bc.Course)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BranchCourseForResultDto>>(branchCourseData);
    }

    public async Task<BranchCourseForResultDto> RetrieveByIdAsync(long id)
    {
        var branchCourseData = await _branchCourseRepository
            .SelectAsync(bc => bc.Id == id);
        if (branchCourseData is null)
            throw new ZeeyoException(404, "BranchCourse is not found");

        return _mapper.Map<BranchCourseForResultDto>(branchCourseData);
    }
}
