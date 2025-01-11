using Zeeyo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Zeeyo.Service.Configurations;
using Zeeyo.Api.Controllers.Commons;
using Zeeyo.Service.Interfaces.Students;
using Zeeyo.Service.DTOs.Students.Students;

namespace Zeeyo.Api.Controllers.Students;

public class StudentsController : BaseController
{
    private readonly IStudentService _studentService;
    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    /// <summary>
    /// To Create new student 
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] StudentForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.AddAsync(dto)
        });

    /// <summary>
    /// To post profile photo of student
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("{studentId}/profilePhoto")]
    public async Task<IActionResult> PostProfilePhotoAsync([FromRoute(Name = "studentId")] long studentId, FormFile formFile)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.AddProfilePhotoAsync(studentId, formFile)
        });

    /// <summary>
    /// To get all students
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// to get all students by Branch
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet("{branchId}/users")]
    public async Task<IActionResult> GetAllByBranchAsync([FromRoute(Name = "branchId")] long branchId, [FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.RetrieveAllByBranchIdAsync(branchId, @params)
        });

    /// <summary>
    /// to get student by id 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}/student")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// to get profile photo of student by userId 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{studentId}/profilePhoto")]
    public async Task<IActionResult> GetProfilePhotoAsync([FromRoute(Name = "studentId")] long studentId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.RetrieveProfilePhotoAsync(studentId)
        });

    /// <summary>
    /// to get student by phone number 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("phoneNumber")]
    public async Task<IActionResult> GetByPhoneNumberAsync([FromQuery(Name = "phoneNumber")] string phoneNumber)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.RetrieveByPhoneNumberAsync(phoneNumber)
        });

    /// <summary>
    /// To update student by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] StudentForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.ModifyAsync(id, dto)
        });

    /// <summary>
    /// To delete student by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}/student")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.RemoveAsync(id)
        });

    /// <summary>
    /// To delete profile photo of student by studentId
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{studentId}/profilePhoto")]
    public async Task<IActionResult> DeleteProfilePhotoAsync([FromRoute(Name = "studentId")] long studentId)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _studentService.RemoveProfilePhotoAsync(studentId)
        });

    /// <summary>
    /// To get all students by searching
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
            Data = await _studentService.SearchAllAsync(search, @params)
        });
}
