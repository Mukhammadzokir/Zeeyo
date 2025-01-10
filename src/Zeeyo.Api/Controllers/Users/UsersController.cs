using Zeeyo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Zeeyo.Service.Configurations;
using Zeeyo.Api.Controllers.Commons;
using Zeeyo.Service.DTOs.Users.Users;
using Zeeyo.Service.Interfaces.Users;
using System.ComponentModel.DataAnnotations;

namespace Zeeyo.Api.Controllers.Users;

public class UsersController : BaseController
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// To Create new user 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] UserForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.AddAsync(dto)
        });

    /// <summary>
    /// To post profile photo of user
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("{userId}/profilePhoto")]
    public async Task<IActionResult> PostProfilePhotoAsync([FromRoute(Name = "userId")] long userId, FormFile formFile)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.AddProfilePhotoAsync(userId, formFile)
        });

    /// <summary>
    /// To get all users
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// to get all users by Branch
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet("{branchId}/users")]
    public async Task<IActionResult> GetAllByBranchAsync([FromRoute(Name = "branchId")] long branchId, [FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.RetrieveAllByBranchIdAsync(branchId,@params)
        });

    /// <summary>
    /// to get user by id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// to get profile photo of user by userId 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{userId}/profilePhoto")]
    public async Task<IActionResult> GetProfilePhotoAsync([FromRoute(Name = "userId")] long userId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.RetrieveProfilePhotoAsync(userId)
        });

    /// <summary>
    /// to get user by phone number 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("phoneNumber")]
    public async Task<IActionResult> GetByPhoneNumberAsync([FromQuery(Name = "phoneNumber")] string phoneNumber)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.RetrieveByPhoneNumberAsync(phoneNumber)
        });

    /// <summary>
    /// To update user by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] UserForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.ModifyAsync(id, dto)
        });

    /// <summary>
    /// To delete by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.RemoveAsync(id)
        });

    /// <summary>
    /// To delete profile photo of user by userId
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{userId}/profilePhoto")]
    public async Task<IActionResult> DeleteProfilePhotoAsync([FromRoute(Name = "userId")] long userId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.RemoveProfilePhotoAsync(userId)
        });

    /// <summary>
    /// To get all users by searching
    /// </summary>
    /// <param name="search"></param>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet("{search}")]
    public async Task<IActionResult> GetAllBySearchAsync([FromQuery(Name = "search")] string search, [FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.SearchAllAsync(search, @params)
        });

    /// <summary>
    /// To Change user password
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("change-password")]
    public async Task<ActionResult<UserForResultDto>> ChangePasswordAsync(long id, UserForChangePasswordDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.ChangePasswordAsync(id, dto)
        });

    /// <summary>
    /// To Create new password when forget password
    /// </summary>
    /// <param name="PhoneNumber"></param>
    /// <param name="NewPassword"></param>
    /// <param name="ConfirmPassword"></param>
    /// <returns></returns>
    [HttpPut("forget-password")]
    public async Task<IActionResult> ForgetPasswordAsync([Required] string PhoneNumber, [Required] string NewPassword, [Required] string ConfirmPassword)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _userService.ForgetPasswordAsync(PhoneNumber, NewPassword, ConfirmPassword)
        });
}