using AutoMapper;
using Zeeyo.Service.Helpers;
using Zeeyo.Domain.Extensions;
using Zeeyo.Service.Exceptions;
using Zeeyo.Data.IRepositories;
using Microsoft.AspNetCore.Http;
using Zeeyo.Domain.Entities.Users;
using Zeeyo.Service.Configurations;
using Microsoft.EntityFrameworkCore;
using Zeeyo.Domain.Entities.Branches;
using Zeeyo.Service.DTOs.Users.Users;
using Zeeyo.Service.Interfaces.Teachers;
using Microsoft.Extensions.Configuration;
using Zeeyo.Service.DTOs.Teachers.Teachers;

namespace Zeeyo.Service.Services.Teachers;

public class TeacherService : ITeacherService
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IRepository<User> _teacherRepository;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<UserProfilePhoto> _teacherProfilePhotoRepository;

    public TeacherService(
        IMapper mapper,
        IConfiguration configuration,
        IRepository<User> teacherRepository,
        IRepository<Branch> branchRepository,
        IRepository<UserProfilePhoto> teacherProfilePhotoRepository)
    {
        _mapper = mapper;
        _configuration = configuration;
        _branchRepository = branchRepository;
        _teacherRepository = teacherRepository;
        _teacherProfilePhotoRepository = teacherProfilePhotoRepository;
    }
    public async Task<TeacherForResultDto> AddAsync(TeacherForCreationDto dto)
    {
        var branchData = await _branchRepository
            .SelectAsync(b => b.Id == dto.BranchId);
        if (branchData is null)
            throw new ZeeyoException(404, "Branch is not found");

        var teacherData = await _teacherRepository
            .SelectAsync(t => t.PhoneNumber == dto.PhoneNumber);
        if (teacherData is not null)
            throw new ZeeyoException(409, "Teacher is already exist");

        var hasherResult = PasswordHelper.Hash(dto.Password);
        var mappedData = _mapper.Map<User>(dto);

        mappedData.CreatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.Salt = hasherResult.Salt;
        mappedData.Password = hasherResult.Hash;

        var result = await _teacherRepository.InsertAsync(mappedData);

        return _mapper.Map<TeacherForResultDto>(result);
    }

    public async Task<TeacherProfilePhotoForResultDto> AddProfilePhotoAsync(long teacherId, IFormFile formFile)
    {
        var userData = await _teacherRepository
          .SelectAsync(t => t.Id == teacherId);
        if (userData is null)
            throw new ZeeyoException(404, "Teacher is not found");

        if (formFile.Length > 5000000)
            throw new ZeeyoException(400, "Size of photo must be less than 5 mb");

        var extensions = new string[] { ".jpg", ".png", ".jpeg", ".heic", ".heif" };
        var extensionOfPhoto = Path.GetExtension(formFile.FileName);
        if (!extensions.Contains(extensionOfPhoto))
        {
            throw new ZeeyoException(400, "Extension of photo must be .jpg, .png, .jpeg, .heic or .heif");
        }

        var teacherProfilePhotoData = await _teacherProfilePhotoRepository
            .SelectAsync(tp => tp.Id == teacherId);
        if (teacherProfilePhotoData is not null)
        {
            await RemoveProfilePhotoAsync(teacherId);
        }

        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(formFile.FileName);
        var rootPath = Path.Combine(EnvironmentHelper.WebRootPath, "Teachers", "ProfilePhotos", fileName);
        using (var stream = new FileStream(rootPath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Close();
        }

        var mappedAsset = new UserProfilePhoto()
        {
            Id = teacherId,
            Name = fileName,
            Path = Path.Combine("Teachers", "ProfilePhotos", formFile.FileName),
            Extension = Path.GetExtension(formFile.FileName),
            Size = formFile.Length,
            Type = formFile.ContentType,
            CreatedAt = DateTime.UtcNow,
        };

        mappedAsset.CreatedAt = TimeHelper.GetCurrentServerTime();

        var result = await _teacherProfilePhotoRepository.InsertAsync(mappedAsset);

        return _mapper.Map<TeacherProfilePhotoForResultDto>(result);
    }

    public async Task<bool> ChangePasswordAsync(long id, UserForChangePasswordDto dto)
    {
        var teacherData = await _teacherRepository.SelectAsync(t => t.Id == id);
        if (teacherData is null || !PasswordHelper.Verify(dto.OldPassword, teacherData.Salt, teacherData.Password))
            throw new ZeeyoException(404, "Teacher or Password is incorrect");
        else if (dto.NewPassword != dto.ConfirmPassword)
            throw new ZeeyoException(400, "New password and confirm password aren't equal");

        var hash = PasswordHelper.Hash(dto.ConfirmPassword);
        teacherData.Salt = hash.Salt;
        teacherData.Password = hash.Hash;

        await _teacherRepository.UpdateAsync(teacherData);

        return true;
    }

    public async Task<bool> ForgetPasswordAsync(string PhoneNumber, string NewPassword, string ConfirmPassword)
    {
        var teacherData = await _teacherRepository.SelectAsync(t => t.PhoneNumber == PhoneNumber);

        if (teacherData is null)
            throw new ZeeyoException(404, "Teacher not found");

        if (NewPassword != ConfirmPassword)
            throw new ZeeyoException(400, "New password and confirm password aren't equal");

        var hash = PasswordHelper.Hash(NewPassword);

        teacherData.Salt = hash.Salt;
        teacherData.Password = hash.Hash;

        await _teacherRepository.UpdateAsync(teacherData);

        return true;
    }

    public async Task<TeacherForResultDto> ModifyAsync(long id, TeacherForUpdateDto dto)
    {
        var branchData = await _branchRepository.SelectAsync(b => b.Id == dto.BranchId);
        if (branchData is null)
            throw new ZeeyoException(404, "Branch is not found");

        var teacherData = await _teacherRepository.SelectAsync(t => t.Id == id);
        if (teacherData is null)
            throw new ZeeyoException(404, "Teacher is not found");

        var mappedData = _mapper.Map(dto, teacherData);

        mappedData.UpdatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.UpdatedBy = HttpContextHelper.UserId;

        await _teacherRepository.UpdateAsync(mappedData);

        return _mapper.Map<TeacherForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var teacherData = await _teacherRepository.SelectAsync(t => t.Id == id);

        if (teacherData is null)
            throw new ZeeyoException(404, "Teacher is not found");
        
        teacherData.DeletedAt = TimeHelper.GetCurrentServerTime();
        teacherData.DeletedBy = HttpContextHelper.UserId;

        return await _teacherRepository.DeleteAsync(id);
    }

    public async Task<bool> RemoveProfilePhotoAsync(long teacherId)
    {
        var teacherData = await _teacherRepository
            .SelectAsync(t => t.Id == teacherId);
        if (teacherData is null)
            throw new ZeeyoException(404, "Teacher is not found");

        var teacherProfilePhotoData = await _teacherProfilePhotoRepository
            .SelectAsync(tp => tp.Id == teacherId);
        if (teacherProfilePhotoData is null)
            throw new ZeeyoException(404, "TeacherProfilePhoto is not found");

        teacherProfilePhotoData.DeletedAt = TimeHelper.GetCurrentServerTime();
        teacherProfilePhotoData.DeletedBy = HttpContextHelper.UserId;

        return await _teacherProfilePhotoRepository.DeleteAsync(teacherProfilePhotoData.Id);
    }

    public async Task<IEnumerable<TeacherForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var teacherData = await _teacherRepository
           .SelectAll()
           .Where(t => t.UserRoles.Any(tr => tr.Role.Name == "Teacher"))
           .AsNoTracking()
           .ToPagedList(@params)
           .ToListAsync();

        return _mapper.Map<IEnumerable<TeacherForResultDto>>(teacherData);
    }

    public async Task<IEnumerable<TeacherForResultDto>> RetrieveAllByBranchIdAsync(long branchId, PaginationParams @params)
    {
        var teacherData = await _teacherRepository
            .SelectAll()
            .Where(t => t.BranchId == branchId && t.UserRoles.Any(tr => tr.Role.Name == "Teacher"))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<TeacherForResultDto>>(teacherData);
    }

    public async Task<TeacherForResultDto> RetrieveByIdAsync(long id)
    {
        var teacherData = await _teacherRepository
            .SelectAsync(t => t.Id == id && t.UserRoles.Any(tr => tr.Role.Name == "Teacher"));

        if (teacherData is null)
            throw new ZeeyoException(404, "Teacher is not found");

        return _mapper.Map<TeacherForResultDto>(teacherData);
    }

    public async Task<TeacherForResultDto> RetrieveByPhoneNumberAsync(string phoneNumber)
    {
        var teacherData = await _teacherRepository
            .SelectAsync(t => t.PhoneNumber == phoneNumber && t.UserRoles.Any(tr => tr.Role.Name == "Teacher"));

        if (teacherData is null)
            throw new ZeeyoException(404, "Teacher is not found");

        return _mapper.Map<TeacherForResultDto>(teacherData);
    }

    public async Task<TeacherProfilePhotoForResultDto> RetrieveProfilePhotoAsync(long teacherId)
    {
        var teacherData = await _teacherRepository
            .SelectAsync(t => t.Id == teacherId && t.UserRoles.Any(tr => tr.Role.Name == "Teacher"));
        if (teacherData is null)
            throw new ZeeyoException(404, "Teacher is not found");

        var teacherProfilePhotoData = await _teacherProfilePhotoRepository
            .SelectAsync(tp => tp.UserId == teacherId);
        if (teacherProfilePhotoData is null)
            throw new ZeeyoException(404, "TeacherProfilePhoto is not found");

        return _mapper.Map<TeacherProfilePhotoForResultDto>(teacherProfilePhotoData);
    }

    public async Task<IEnumerable<TeacherForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var teacherData = await _teacherRepository
           .SelectAll()
           .Where(t => t.UserRoles.Any(tr => tr.Role.Name == "Teacher")
                               || t.FirstName.ToLower().Contains(search.ToLower())
                               || t.LastName.ToLower().Contains(search.ToLower())
                               || t.PhoneNumber.Contains(search))
           .AsNoTracking()
           .ToPagedList(@params)
           .ToListAsync();

        return _mapper.Map<IEnumerable<TeacherForResultDto>>(teacherData);
    }
}