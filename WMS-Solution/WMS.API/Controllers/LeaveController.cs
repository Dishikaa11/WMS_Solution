using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveController : ControllerBase
{
    private readonly ILeaveService _leaveService;

    public LeaveController(ILeaveService leaveService)
    {
        _leaveService = leaveService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _leaveService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var leave = await _leaveService.GetByIdAsync(id);

        if (leave == null)
            return NotFound();

        return Ok(leave);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create(LeaveDto dto)
    {
        await _leaveService.AddAsync(dto);

        return Ok(new
        {
            message = "Leave Applied Successfully"
        });
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, LeaveDto dto)
    {
        await _leaveService.UpdateAsync(id, dto);

        return Ok(new
        {
            message = "Leave Updated Successfully"
        });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _leaveService.DeleteAsync(id);

        return Ok(new
        {
            message = "Leave Deleted Successfully"
        });
    }

    [Authorize(Roles = "Manager,Admin")]
    [HttpPut("approve/{leaveId}")]
    public async Task<IActionResult> Approve(
    int leaveId,
    int managerId)
    {
        await _leaveService.ApproveAsync(
            leaveId,
            managerId);

        return Ok(new
        {
            message = "Leave Approved"
        });
    }

    [Authorize(Roles = "Manager,Admin")]
    [HttpPut("reject/{leaveId}")]
    public async Task<IActionResult> Reject(
    int leaveId,
    int managerId)
    {
        await _leaveService.RejectAsync(
            leaveId,
            managerId);

        return Ok(new
        {
            message = "Leave Rejected"
        });
    }

    [Authorize]
    [HttpPut("cancel/{id}")]
    public async Task<IActionResult> Cancel(int id)
    {
        await _leaveService.CancelAsync(id);

        return Ok(new
        {
            Message = "Leave cancelled successfully"
        });
    }

//     [Authorize(Roles = "Employee")]
// [HttpGet("my-leaves")]
// public async Task<IActionResult> GetMyLeaves()
// {
//     var username =
//         User.FindFirst(
//             ClaimTypes.Name
//         )?.Value;

//     if (username == null)
//         return Unauthorized();

//     var result =
//         await _leaveService
//             .GetMyLeavesAsync(username);

//     return Ok(result);
// }
}