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
using Zeeyo.Service.Interfaces.Students;
using Microsoft.Extensions.Configuration;
using Zeeyo.Service.DTOs.Students.Students;

namespace Zeeyo.Service.Services.Students;

public class StudentService : IStudentService
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IRepository<User> _studentRepository;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<UserProfilePhoto> _studentProfilePhotoRepository;

    public StudentService(
        IMapper mapper,
        IConfiguration configuration,
        IRepository<User> studentRepository,
        IRepository<Branch> branchRepository,
        IRepository<UserProfilePhoto> studentProfilePhotoRepository)
    {
        _mapper = mapper;
        _configuration = configuration;
        _studentRepository = studentRepository;
        _branchRepository = branchRepository;
        _studentProfilePhotoRepository = studentProfilePhotoRepository;
    }
    public async Task<StudentForResultDto> AddAsync(StudentForCreationDto dto)
    {
        var branchData = await _branchRepository.SelectAsync(b => b.Id == dto.BranchId);
        if (branchData is null)
            throw new ZeeyoException(404, "Branch is not found");

        var studentData = await _studentRepository.SelectAsync(s => s.PhoneNumber == dto.PhoneNumber);
        if (studentData is not null)
            throw new ZeeyoException(409, "Student is already exist");

        var hasherResult = PasswordHelper.Hash(dto.Password);
        var mappedData = _mapper.Map<User>(dto);

        mappedData.CreatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.Salt = hasherResult.Salt;
        mappedData.Password = hasherResult.Hash;

        var result = await _studentRepository.InsertAsync(mappedData);

        return _mapper.Map<StudentForResultDto>(result);
    }

    public async Task<StudentProfilePhotoForResultDto> AddProfilePhotoAsync(long studentId, IFormFile formFile)
    {
        var studentData = await _studentRepository.SelectAsync(s => s.Id == studentId);

        if (studentData is null)
            throw new ZeeyoException(404, "Student is not found");

        if (formFile.Length > 5000000)
            throw new ZeeyoException(400, "Size of photo must be less than 5 mb");

        var extensions = new string[] { ".jpg", ".png", ".jpeg", ".heic", ".heif" };
        var extensionOfPhoto = Path.GetExtension(formFile.FileName);
        if (!extensions.Contains(extensionOfPhoto))
        {
            throw new ZeeyoException(400, "Extension of photo must be .jpg, .png, .jpeg, .heic or .heif");
        }

        var studentProfilePhotoData = await _studentProfilePhotoRepository.SelectAsync(sp => sp.Id == studentId);
        if (studentProfilePhotoData is not null)
        {
            await RemoveProfilePhotoAsync(studentId);
        }

        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(formFile.FileName);
        var rootPath = Path.Combine(EnvironmentHelper.WebRootPath, "Students", "ProfilePhotos", fileName);
        using (var stream = new FileStream(rootPath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Close();
        }

        var mappedAsset = new UserProfilePhoto()
        {
            Id = studentId,
            Name = fileName,
            Path = Path.Combine("Students", "ProfilePhotos", formFile.FileName),
            Extension = Path.GetExtension(formFile.FileName),
            Size = formFile.Length,
            Type = formFile.ContentType,
            CreatedAt = DateTime.UtcNow,
        };

        mappedAsset.CreatedAt = TimeHelper.GetCurrentServerTime();

        var result = await _studentProfilePhotoRepository.InsertAsync(mappedAsset);

        return this._mapper.Map<StudentProfilePhotoForResultDto>(result);
    }

    public async Task<StudentForResultDto> ModifyAsync(long id, StudentForUpdateDto dto)
    {
        var branchData = await _branchRepository.SelectAsync(b => b.Id == dto.BranchId);

        if (branchData is null)
            throw new ZeeyoException(404, "Branch is not found");

        var studentData = await _studentRepository.SelectAsync(s => s.Id == id);
        if (studentData is null)
            throw new ZeeyoException(404, "Student is not found");

        var mappedData = _mapper.Map(dto, studentData);

        mappedData.UpdatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.UpdatedBy = HttpContextHelper.UserId;

        await _studentRepository.UpdateAsync(mappedData);

        return _mapper.Map<StudentForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var studentData = await _studentRepository.SelectAsync(s => s.Id == id);

        if (studentData is null)
            throw new ZeeyoException(404, "Student is not found");

        studentData.DeletedAt = TimeHelper.GetCurrentServerTime();
        studentData.DeletedBy = HttpContextHelper.UserId;

        return await _studentRepository.DeleteAsync(id);
    }

    public async Task<bool> RemoveProfilePhotoAsync(long studentId)
    {
        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == studentId);
        if (studentData is null)
            throw new ZeeyoException(404, "Student is not found");

        var studentProfilePhotoData = await _studentProfilePhotoRepository
            .SelectAsync(sp => sp.Id == studentId);
        if (studentProfilePhotoData is null)
            throw new ZeeyoException(404, "StudentProfilePhoto is not found");

        studentProfilePhotoData.DeletedAt = TimeHelper.GetCurrentServerTime();
        studentProfilePhotoData.DeletedBy = HttpContextHelper.UserId;

        return await _studentProfilePhotoRepository.DeleteAsync(studentProfilePhotoData.Id);
    }

    public async Task<IEnumerable<StudentForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var studentData = await _studentRepository
            .SelectAll()
            .Where(s => s.UserRoles.Any(sr => sr.Role.Name == "Student"))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<StudentForResultDto>>(studentData);
    }

    public async Task<IEnumerable<StudentForResultDto>> RetrieveAllByBranchIdAsync(long branchId, PaginationParams @params)
    {
        var studentData = await _studentRepository
            .SelectAll()
            .Where(s => s.BranchId == branchId && s.UserRoles.Any(sr => sr.Role.Name == "Student"))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<StudentForResultDto>>(studentData);
    }

    public async Task<StudentForResultDto> RetrieveByIdAsync(long id)
    {
        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == id && s.UserRoles.Any(sr => sr.Role.Name == "Student"));

        if (studentData is null)
            throw new ZeeyoException(404, "Student is not found");

        return _mapper.Map<StudentForResultDto>(studentData);
    }

    public async Task<StudentForResultDto> RetrieveByPhoneNumberAsync(string phoneNumber)
    {
        var studentData = await _studentRepository
            .SelectAsync(s => s.PhoneNumber == phoneNumber && s.UserRoles.Any(sr => sr.Role.Name == "Student"));

        if (studentData is null)
            throw new ZeeyoException(404, "Student is not found");

        return _mapper.Map<StudentForResultDto>(studentData);
    }

    public async Task<StudentProfilePhotoForResultDto> RetrieveProfilePhotoAsync(long studentId)
    {
        var studentData = await _studentRepository
            .SelectAsync(s => s.Id == studentId && s.UserRoles.Any(sr => sr.Role.Name == "Student"));
        if (studentData is null)
            throw new ZeeyoException(404, "Student is not found");

        var studentProfilePhotoData = await _studentProfilePhotoRepository
            .SelectAsync(sp => sp.UserId == studentId);
        if (studentProfilePhotoData is null)
            throw new ZeeyoException(404, "StudentProfilePhoto is not found");

        return _mapper.Map<StudentProfilePhotoForResultDto>(studentProfilePhotoData);
    }

    public async Task<IEnumerable<StudentForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var studentData = await _studentRepository
           .SelectAll()
           .Where(s => s.UserRoles.Any(sr => sr.Role.Name == "Student")
                               || s.FirstName.ToLower().Contains(search.ToLower())
                               || s.LastName.ToLower().Contains(search.ToLower())
                               || s.PhoneNumber.Contains(search))
           .AsNoTracking()
           .ToPagedList(@params)
           .ToListAsync();

        return _mapper.Map<IEnumerable<StudentForResultDto>>(studentData);
    }
}
