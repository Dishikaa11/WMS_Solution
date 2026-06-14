using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeProjectAllocationController
    : ControllerBase
{
    private readonly IEmployeeProjectAllocationService
        _allocationService;

    public EmployeeProjectAllocationController(
        IEmployeeProjectAllocationService allocationService)
    {
        _allocationService = allocationService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(
            await _allocationService.GetAllAsync());
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var allocation =
            await _allocationService.GetByIdAsync(id);

        if (allocation == null)
            return NotFound();

        return Ok(allocation);
    }
    [Authorize(Roles = "Admin,Manager")]
    [HttpPost]
    public async Task<IActionResult> Create(
        EmployeeProjectAllocationDto dto)
    {
        await _allocationService.AddAsync(dto);

        return Ok(new
        {
            Message =
                "Employee Project Allocation Added Successfully"
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        EmployeeProjectAllocationDto dto)
    {
        await _allocationService.UpdateAsync(id, dto);

        return Ok(new
        {
            Message =
                "Employee Project Allocation Updated Successfully"
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _allocationService.DeleteAsync(id);

        return Ok(new
        {
            Message =
                "Employee Project Allocation Deleted Successfully"
        });
    }
}