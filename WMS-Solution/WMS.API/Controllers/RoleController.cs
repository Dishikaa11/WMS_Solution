using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers;

[Authorize][Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _roleService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _roleService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(RoleDto dto)
    {
        await _roleService.AddAsync(dto);

        return Ok("Role Created Successfully");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        RoleDto dto)
    {
        await _roleService.UpdateAsync(id, dto);

        return Ok("Role Updated Successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _roleService.DeleteAsync(id);

        return Ok("Role Deleted Successfully");
    }
}