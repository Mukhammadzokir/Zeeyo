using AutoMapper;
using Zeeyo.Service.Helpers;
using Zeeyo.Domain.Extensions;
using Zeeyo.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Zeeyo.Data.IRepositories;
using Zeeyo.Domain.Entities.Users;
using Zeeyo.Service.Configurations;
using Microsoft.EntityFrameworkCore;
using Zeeyo.Service.DTOs.Users.Users;
using Zeeyo.Domain.Entities.Branches;
using Zeeyo.Service.Interfaces.Users;
using Microsoft.Extensions.Configuration;

namespace Zeeyo.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Branch> _branchRepository;
    private readonly IRepository<UserProfilePhoto> _userProfilePhotoRepository;

    public UserService(
        IMapper mapper,
        IConfiguration configuration,
        IRepository<User> userRepository,
        IRepository<Branch> branchRepository,
        IRepository<UserProfilePhoto> userProfilePhotoRepository)
    {
        _mapper = mapper;
        _configuration = configuration;
        _userRepository = userRepository;
        _branchRepository = branchRepository;
        _userProfilePhotoRepository = userProfilePhotoRepository;
    }

    public async Task<UserForResultDto> AddAsync(UserForCreationDto dto)
    {
        var branchData = await _branchRepository
            .SelectAsync(b => b.Id == dto.BranchId);
        if (branchData is null)
            throw new ZeeyoException(404, "Branch is not found");

        var userData = await _userRepository
            .SelectAsync(u => u.PhoneNumber == dto.PhoneNumber);
        if (userData is not null)
            throw new ZeeyoException(409, "User is already found");

        var hasherResult = PasswordHelper.Hash(dto.Password);
        var mappedData = _mapper.Map<User>(dto);

        mappedData.CreatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.Salt = hasherResult.Salt;
        mappedData.Password = hasherResult.Hash;

        var result = await _userRepository.InsertAsync(mappedData);

        return _mapper.Map<UserForResultDto>(result);
    }

    public async Task<UserProfilePhotoForResultDto> AddProfilePhotoAsync(long userId, IFormFile formFile)
    {
        var userData = await _userRepository
           .SelectAsync(s => s.Id == userId);
        if (userData is null)
            throw new ZeeyoException(404, "User is not found");

        if (formFile.Length > 5000000)
            throw new ZeeyoException(400, "Size of photo must be less than 5 mb");

        var extensions = new string[] { ".jpg", ".png", ".jpeg", ".heic", ".heif" };
        var extensionOfPhoto = Path.GetExtension(formFile.FileName);
        if (!extensions.Contains(extensionOfPhoto))
        {
            throw new ZeeyoException(400, "Extension of photo must be .jpg, .png, .jpeg, .heic or .heif");
        }

        var studentProfilePhotoData = await _userProfilePhotoRepository
            .SelectAsync(up => up.Id == userId);
        if (studentProfilePhotoData is not null)
        {
            await RemoveProfilePhotoAsync(userId);
        }

        var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(formFile.FileName);
        var rootPath = Path.Combine(EnvironmentHelper.WebRootPath, "Users", "ProfilePhotos", fileName);
        using (var stream = new FileStream(rootPath, FileMode.Create))
        {
            await formFile.CopyToAsync(stream);
            await stream.FlushAsync();
            stream.Close();
        }

        var mappedAsset = new UserProfilePhoto()
        {
            Id = userId,
            Name = fileName,
            Path = Path.Combine("Users", "ProfilePhotos", formFile.FileName),
            Extension = Path.GetExtension(formFile.FileName),
            Size = formFile.Length,
            Type = formFile.ContentType,
            CreatedAt = DateTime.UtcNow,
        };

        mappedAsset.CreatedAt = TimeHelper.GetCurrentServerTime();

        var result = await _userProfilePhotoRepository.InsertAsync(mappedAsset);

        return this._mapper.Map<UserProfilePhotoForResultDto>(result);
    }

    public async Task<bool> ChangePasswordAsync(long id, UserForChangePasswordDto dto)
    {
        var userData = await _userRepository.SelectAsync(u => u.Id == id);
        if (userData is null || !PasswordHelper.Verify(dto.OldPassword, userData.Salt, userData.Password))
            throw new ZeeyoException(404, "User or Password is incorrect");
        else if (dto.NewPassword != dto.ConfirmPassword)
            throw new ZeeyoException(400, "New password and confirm password aren't equal");

        var hash = PasswordHelper.Hash(dto.ConfirmPassword);
        userData.Salt = hash.Salt;
        userData.Password = hash.Hash;

        await _userRepository.UpdateAsync(userData);

        return true;
    }

    public async Task<bool> ForgetPasswordAsync(string PhoneNumber, string NewPassword, string ConfirmPassword)
    {
        var userData = await _userRepository.SelectAsync(u => u.PhoneNumber == PhoneNumber);

        if (userData is null)
            throw new ZeeyoException(404, "User not found");

        if (NewPassword != ConfirmPassword)
            throw new ZeeyoException(400, "New password and confirm password aren't equal");

        var hash = PasswordHelper.Hash(NewPassword);

        userData.Salt = hash.Salt;
        userData.Password = hash.Hash;

        await _userRepository.UpdateAsync(userData);

        return true;
    }

    public async Task<UserForResultDto> ModifyAsync(long id, UserForUpdateDto dto)
    {
        var branchData = await _branchRepository.SelectAsync(b => b.Id == dto.BranchId);

        if (branchData is null)
            throw new ZeeyoException(404, "Branch is not found");

        var userData = await _userRepository.SelectAsync(u => u.Id == id);
        if (userData is null)
            throw new ZeeyoException(404, "User is not found");

        var mappedData = _mapper.Map(dto, userData);

        mappedData.UpdatedAt = TimeHelper.GetCurrentServerTime();
        mappedData.UpdatedBy = HttpContextHelper.UserId;

        await _userRepository.UpdateAsync(mappedData);
       
        return _mapper.Map<UserForResultDto>(mappedData);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var userData = await _userRepository.SelectAsync(u => u.Id == id);

        if (userData is null)
            throw new ZeeyoException(404, "User is not found");

        return await _userRepository.DeleteAsync(id);
    }

    public async Task<bool> RemoveProfilePhotoAsync(long userId)
    {
        var userData = await _userRepository
            .SelectAsync(u => u.Id == userId);
        if (userData is null)
            throw new ZeeyoException(404, "User is not found");

        var userProfilePhotoData = await _userProfilePhotoRepository
            .SelectAsync(up => up.Id == userId);
        if (userProfilePhotoData is null)
            throw new ZeeyoException(404, "UserProfilePhoto is not found");

        var userProfilePhotoId = (await _userProfilePhotoRepository
            .SelectAsync(up => up.Id == userId))
            .Id;

        return await _userProfilePhotoRepository.DeleteAsync(userProfilePhotoId);
    }

    public async Task<IEnumerable<UserForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var userData = await _userRepository
            .SelectAll(u => !u.IsDeleted)
            //.Where(u => u.UserRoles.Any(ur => ur.Role.Name == "User"))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserForResultDto>>(userData);
    }

    public async Task<IEnumerable<UserForResultDto>> RetrieveAllByBranchIdAsync(long branchId, PaginationParams @params)
    {
        var userData = await _userRepository
            .SelectAll(u => !u.IsDeleted)
            .Where(u => u.BranchId == branchId)
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserForResultDto>>(userData);
    }

    public async Task<UserForResultDto> RetrieveByIdAsync(long id)
    {
        var userData = await _userRepository.SelectAsync(u => u.Id == id);

        if (userData is null)
            throw new ZeeyoException(404, "User is not found");

        return _mapper.Map<UserForResultDto>(userData);
    }

    public async Task<UserForResultDto> RetrieveByPhoneNumberAsync(string phoneNumber)
    {
        var userData = await _userRepository.SelectAsync(s => s.PhoneNumber == phoneNumber);

        if (userData is null)
            throw new ZeeyoException(404, "User is not found");

        return _mapper.Map<UserForResultDto>(userData);
    }

    public async Task<UserProfilePhotoForResultDto> RetrieveProfilePhotoAsync(long userId)
    {
        var userData = await _userRepository.SelectAsync(u => u.Id == userId);

        if (userData is null)
            throw new ZeeyoException(404, "User is not found");

        var userProfilePhotoData = await _userProfilePhotoRepository.SelectAsync(up => up.UserId == userId);

        if (userProfilePhotoData is null)
            throw new ZeeyoException(404, "UserProfilePhoto is not found");

        return _mapper.Map<UserProfilePhotoForResultDto>(userProfilePhotoData);
    }

    public async Task<IEnumerable<UserForResultDto>> SearchAllAsync(string search, PaginationParams @params)
    {
        var userData = await _userRepository
            .SelectAll(u => !u.IsDeleted)
            .Where(u => u.FirstName.ToLower().Contains(search.ToLower())
                                || u.LastName.ToLower().Contains(search.ToLower())
                                || u.PhoneNumber.Contains(search))
            .AsNoTracking()
            .ToPagedList(@params)
            .ToListAsync();

        return _mapper.Map<IEnumerable<UserForResultDto>>(userData);
    }

}
