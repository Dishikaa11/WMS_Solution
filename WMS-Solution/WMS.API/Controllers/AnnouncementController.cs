using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnnouncementController : ControllerBase
{
    private readonly IAnnouncementService _announcementService;

    public AnnouncementController(
        IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var announcements =
            await _announcementService.GetAllAsync();

        return Ok(announcements);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var announcement =
            await _announcementService.GetByIdAsync(id);

        if (announcement == null)
            return NotFound();

        return Ok(announcement);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create(
        AnnouncementDto dto)
    {
        await _announcementService.AddAsync(dto);

        return Ok(new
        {
            Message = "Announcement Created Successfully"
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        AnnouncementDto dto)
    {
        await _announcementService.UpdateAsync(id, dto);

        return Ok(new
        {
            Message = "Announcement Updated Successfully"
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _announcementService.DeleteAsync(id);

        return Ok(new
        {
            Message = "Announcement Deleted Successfully"
        });
    }
}