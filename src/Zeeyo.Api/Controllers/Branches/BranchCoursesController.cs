using Zeeyo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Zeeyo.Service.Configurations;
using Zeeyo.Api.Controllers.Commons;
using Zeeyo.Service.Interfaces.Branches;
using Zeeyo.Service.DTOs.Branches.BranchCourses;

namespace Zeeyo.Api.Controllers.Branches;

public class BranchCoursesController : BaseController
{
    private readonly IBranchCourseService _branchCourseService;
    public BranchCoursesController(IBranchCourseService branchCourseService)
    {
        _branchCourseService = branchCourseService;
    }

    /// <summary>
    /// To Create
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] BranchCourseForCreationDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _branchCourseService.AddAsync(dto)
        });

    /// <summary>
    /// To Get all 
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _branchCourseService.RetrieveAllAsync(@params)
        });

    /// <summary>
    /// To Get by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute(Name = "id")] long id)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _branchCourseService.RetrieveByIdAsync(id)
        });

    /// <summary>
    /// To update by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync([FromRoute(Name = "id")] long id, [FromBody] BranchCourseForUpdateDto dto)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await _branchCourseService.ModifyAsync(id, dto)
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
            Data = await _branchCourseService.RemoveAsync(id)
        });
}
