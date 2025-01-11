using AutoMapper;
using Zeeyo.Service.Helpers;
using Zeeyo.Domain.Extensions;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.Exceptions;
using Zeeyo.Domain.Entities.Roles;
using Zeeyo.Service.Configurations;
using Microsoft.EntityFrameworkCore;
using Zeeyo.Service.Interfaces.Roles;
using Zeeyo.Service.DTOs.Roles.RolePermissions;

namespace Zeeyo.Service.Services.Roles;

public class RolePermissionService : IRolePermissionService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Role> _roleRepository;
    private readonly IRepository<Permission> _permissionRepository;
    private readonly IRepository<RolePermission> _rolePermissionRepository;

    public RolePermissionService(
        IMapper mapper,
        IRepository<Role> roleRepository,
        IRepository<Permission> permissionRepository,
        IRepository<RolePermission> rolePermissionRepository)
    {
        _mapper = mapper;
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _rolePermissionRepository = rolePermissionRepository;
    }

    public async Task<RolePermissionForResultDto> AddAsync(RolePermissionForCreationDto dto)
    {
        var roleData = await _roleRepository
            .SelectAsync(r => r.Id == dto.RoleId);
        if (roleData is null)
            throw new ZeeyoException(404, "Role is not found");

        var permissionData = await _permissionRepository
            .SelectAsync(p => p.Id == dto.PermissionId);
        if (permissionData is null)
            throw new ZeeyoException(404, "Permission is not found");

        var rolePermissionData = await _rolePermissionRepository
            .SelectAsync(rp => rp.RoleId == dto.RoleId && rp.PermissionId == dto.PermissionId);
        if (rolePermissionData is not null)
            throw new ZeeyoException(404, "RolePermission is already exist");

        var mappedData = _mapper.Map<RolePermission>(dto);
        mappedData.CreatedAt = TimeHelper.GetCurrentServerTime();

        return _mapper.Map<RolePermissionForResultDto>(await _rolePermissionRepository.InsertAsync(mappedData));
    }

    public async Task<RolePermissionForResultDto> ModifyAsync(long id, RolePermissionForUpdateDto dto)
    {
        var roleData = await _roleRepository
            .SelectAsync(r => r.Id == dto.RoleId);
        if (roleData is null)
            throw new ZeeyoException(404, "Role is not found");

        var permissionData = await _permissionRepository
            .SelectAsync(p => p.Id == dto.PermissionId);
        if (permissionData is null)
            throw new ZeeyoException(404, "Permission is not found");

        var rolePermissionData = await _rolePermissionRepository
            .SelectAsync(rp => rp.Id == id);
        if (rolePermissionData is null)
            throw new ZeeyoException(404, "RolePermission is not found");

        var mappedData = _mapper.Map(dto, rolePermissionData);
        mappedData.UpdatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.UpdatedBy = HttpContextHelper.UserId;

        await _rolePermissionRepository.UpdateAsync(mappedData);

        return _mapper.Map<RolePermissionForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var rolePermissionData = await _rolePermissionRepository
            .SelectAsync(rp => rp.Id == id);
        if (rolePermissionData is null)
            throw new ZeeyoException(404, "RolePermission is not found");

        rolePermissionData.DeletedAt = TimeHelper.GetCurrentServerTime();
        rolePermissionData.DeletedBy = HttpContextHelper.UserId;

        return await _rolePermissionRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<RolePermissionForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var rolePermissionData = await _rolePermissionRepository
            .SelectAll()
            .Include(rp => rp.Role)
            .Include(rp => rp.Permission)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<RolePermissionForResultDto>>(rolePermissionData);
    }

    public async Task<RolePermissionForResultDto> RetrieveByIdAsync(long id)
    {
        var rolePermissionData = await _rolePermissionRepository
            .SelectAll()
            .Include(rp => rp.Role)
            .Include(rp => rp.Permission)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (rolePermissionData is null)
            throw new ZeeyoException(404, "RolePermission is not found");

        return _mapper.Map<RolePermissionForResultDto>(rolePermissionData);
    }

    public Task<IEnumerable<RolePermissionForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }
}
