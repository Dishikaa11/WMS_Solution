using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace WMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendanceController : ControllerBase
{
    private readonly IAttendanceService _attendanceService;

    public AttendanceController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var attendances = await _attendanceService.GetAllAsync();
        return Ok(attendances);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var attendance = await _attendanceService.GetByIdAsync(id);

        if (attendance == null)
            return NotFound();

        return Ok(attendance);
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> Create(AttendanceDto dto)
    {
        await _attendanceService.AddAsync(dto);

        return Ok(new
        {
            Message = "Attendance Added Successfully"
        });
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AttendanceDto dto)
    {
        await _attendanceService.UpdateAsync(id, dto);

        return Ok(new
        {
            Message = "Attendance Updated Successfully"
        });
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _attendanceService.DeleteAsync(id);

        return Ok(new
        {
            Message = "Attendance Deleted Successfully"
        });
    }

//     [Authorize(Roles = "Employee")]
// [HttpGet("my-attendance")]
// public async Task<IActionResult> GetMyAttendance()
// {
//     var username =
//         User.FindFirst(
//             ClaimTypes.Name
//         )?.Value;

//     if (username == null)
//         return Unauthorized();

//     var result =
//         await _attendanceService
//             .GetMyAttendanceAsync(username);

//     return Ok(result);
// }
}