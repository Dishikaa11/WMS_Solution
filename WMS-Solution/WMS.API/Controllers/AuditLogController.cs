using Microsoft.AspNetCore.Mvc;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;

namespace WMS.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuditLogController : ControllerBase
{
    private readonly IAuditLogService _service;

    public AuditLogController(IAuditLogService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var audit = await _service.GetByIdAsync(id);

        if (audit == null)
            return NotFound();

        return Ok(audit);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AuditLogDto dto)
    {
        await _service.AddAsync(dto);

        return Ok("Audit Log Created");
    }
}