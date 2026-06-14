using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(
        IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary()
    {
        var result =
            await _dashboardService.GetSummaryAsync();

        return Ok(result);
    }

    [Authorize(Roles = "Employee")]
    [HttpGet("employee")]
    public async Task<IActionResult>
    GetEmployeeDashboard()
    {
        var employeeId =
            int.Parse(
                User.FindFirst("EmployeeId")!
                    .Value);

        return Ok(
            await _dashboardService.GetEmployeeDashboardAsync(employeeId));
    }

    [Authorize]
    [HttpGet("test")]
    public IActionResult Test()
    {
        return Ok("JWT Working");
    }
}