using Microsoft.AspNetCore.Mvc;
using Zeeyo.Api.Controllers.Commons;
using Zeeyo.Api.Models;
using Zeeyo.Service.Configurations;
using Zeeyo.Service.DTOs.Branches.Branches;
using Zeeyo.Service.DTOs.Courses.Courses;
using Zeeyo.Service.Interfaces.Branches;
using Zeeyo.Service.Interfaces.Courses;

namespace Zeeyo.Api.Controllers.Courses;

public class CoursesController : BaseController
{
    private readonly ICourseService _courseService;
    public CoursesController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    /// <summary>
    /// To Create course
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CourseForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _courseService.AddAsync(dto)
        });

    /// <summary>
    /// To Get all courses
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _courseService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// To Get course by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _courseService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// To update course by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] CourseForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _courseService.ModifyAsync(id, dto)
        });

    /// <summary>
    /// To Delete course by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _courseService.RemoveAsync(id)
        });

    /// <summary>
    /// To get all courses by searching
    /// </summary>
    /// <param name="search"></param>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet("search")]
    public async Task<IActionResult> GetAllBySearchAsync([FromQuery(Name = "search")] string search, [FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _courseService.SearchAllAsync(search, @params)
        });
}