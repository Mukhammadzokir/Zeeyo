using AutoMapper;
using Zeeyo.Service.Helpers;
using Zeeyo.Domain.Extensions;
using Zeeyo.Service.Exceptions;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.DTOs.Branches;
using Zeeyo.Service.Configurations;
using Microsoft.EntityFrameworkCore;
using Zeeyo.Domain.Entities.Branches;
using Zeeyo.Service.Interfaces.Branches;

namespace Zeeyo.Service.Services.Branches;

public class BranchService : IBranchService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Branch> _branchRepository;

    public BranchService(IMapper mapper, IRepository<Branch> branchRepository)
    {
        _mapper = mapper;
        _branchRepository = branchRepository;
    }

    public async Task<BranchForResultDto> AddAsync(BranchForCreationDto dto)
    {
        var branchData = await _branchRepository
            .SelectAsync(b => b.Name.ToLower() == dto.Name.ToLower());
        if (branchData is not null)
            throw new ZeeyoException(409, "Branch is already exist");

        var mappedData = _mapper.Map<Branch>(dto);
        return _mapper.Map<BranchForResultDto>(await _branchRepository.InsertAsync(mappedData));
    }

    public async Task<BranchForResultDto> ModifyAsync(long id, BranchForUpdateDto dto)
    {
        var branchData = await _branchRepository
            .SelectAsync(b => b.Id == id);
        if (branchData is null)
            throw new ZeeyoException(404, "Branch is not found");

        var mappedData = _mapper.Map(dto, branchData);
        mappedData.UpdatedAt = DateTime.UtcNow;
        mappedData.UpdatedBy = HttpContextHelper.UserId;

        await _branchRepository.UpdateAsync(mappedData);

        return _mapper.Map<BranchForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var branchData = await _branchRepository
            .SelectAsync(b => b.Id == id);
        if (branchData is null)
            throw new ZeeyoException(404, "Branch is not found");

        branchData.DeletedBy = HttpContextHelper.UserId;
         
        return await _branchRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<BranchForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var branchData = await _branchRepository
            .SelectAll()
            //.Include(b => b.Users.Where(u => !u.IsDeleted))
            //.Include(b => b.Courses.Where(c => !c.IsDeleted))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BranchForResultDto>>(branchData);
    }

    public async Task<BranchForResultDto> RetrieveByIdAsync(long id)
    {
        var branchData = await _branchRepository
            .SelectAll()
            .Where(b => b.Id == id)
            //.Include(b => b.Users.Where(u => !u.IsDeleted))
            //.Include(b => b.Courses.Where(c => !c.IsDeleted))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (branchData is null)
            throw new ZeeyoException(404, "Branch is not found");

        return _mapper.Map<BranchForResultDto>(branchData);
    }

    public async Task<IEnumerable<BranchForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var branchData = await _branchRepository
            .SelectAll()
            .Where(b => b.Name.ToLower().Contains(search.ToLower())
                || b.Description.ToLower().Contains(search.ToLower())
                || b.Address.ToLower().Contains(search.ToLower())
                || b.PhoneNumber.Contains(search))
            //.Include(b => b.Users.Where(u => !u.IsDeleted))
            //.Include(b => b.Courses.Where(u => !u.IsDeleted))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<BranchForResultDto>>(branchData);
    }
}
