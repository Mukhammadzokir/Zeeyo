using AutoMapper;
using Zeeyo.Service.Helpers;
using Zeeyo.Domain.Extensions;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.Exceptions;
using Zeeyo.Domain.Entities.Roles;
using Zeeyo.Service.Configurations;
using Microsoft.EntityFrameworkCore;
using Zeeyo.Service.Interfaces.Roles;
using Zeeyo.Service.DTOs.Roles.Permissions;

namespace Zeeyo.Service.Services.Roles;

public class PermissionService : IPermissionService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Permission> _permissionRepository;

    public PermissionService(IMapper mapper, IRepository<Permission> permissionRepository)
    {
        _mapper = mapper;
        _permissionRepository = permissionRepository;
    }

    public async Task<PermissionForResultDto> AddAsync(PermissionForCreationDto dto)
    {
        var permissionData = await _permissionRepository
            .SelectAsync(p => p.Name.ToLower() == dto.Name.ToLower());
        if (permissionData is not null)
            throw new ZeeyoException(409, "Permission is already exist");

        var mappedData = _mapper.Map<Permission>(dto);
        mappedData.CreatedAt = TimeHelper.GetCurrentServerTime();

        return _mapper.Map<PermissionForResultDto>(await _permissionRepository.InsertAsync(mappedData));
    }

    public async Task<PermissionForResultDto> ModifyAsync(long id, PermissionForUpdateDto dto)
    {
        var permissionData = await _permissionRepository
            .SelectAsync(p => p.Id == id);
        if (permissionData is null)
            throw new ZeeyoException(404, "Permission is not found");

        var mappedData = _mapper.Map(dto, permissionData);
        mappedData.UpdatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.UpdatedBy = HttpContextHelper.UserId;

        await _permissionRepository.UpdateAsync(mappedData);

        return _mapper.Map<PermissionForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var permissionData = await _permissionRepository
            .SelectAsync(p => p.Id == id);
        if (permissionData is null)
            throw new ZeeyoException(404, "Permission is not found");

        permissionData.DeletedAt = TimeHelper.GetCurrentServerTime();
        permissionData.DeletedBy = HttpContextHelper.UserId;

        return await _permissionRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<PermissionForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var permissionData = await _permissionRepository
            .SelectAll()
            .Include(P => P.Roles.Where(r => !r.IsDeleted))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PermissionForResultDto>>(permissionData);
    }

    public async Task<PermissionForResultDto> RetrieveByIdAsync(long id)
    {
        var permissionData = await _permissionRepository
            .SelectAll()
            .Include(P => P.Roles.Where(r => !r.IsDeleted))
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (permissionData is null)
            throw new ZeeyoException(404, "Permission is not found");

        return _mapper.Map<PermissionForResultDto>(permissionData);
    }

    public async Task<IEnumerable<PermissionForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var permissionData = await _permissionRepository
            .SelectAll()
            .Where(p => p.Name.ToLower().Contains(search.ToLower()))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PermissionForResultDto>>(permissionData);
    }
}
