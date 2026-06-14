using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class AuditLogService : IAuditLogService
{
    private readonly IAuditLogRepository _repository;

    public AuditLogService(IAuditLogRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<AuditLog>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<AuditLog?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(AuditLogDto dto)
    {
        var audit = new AuditLog
        {
            EntityName = dto.EntityName,
            RecordId = dto.RecordId,
            Action = dto.Action,
            CreatedBy = dto.CreatedBy,
            CreatedOn = DateTime.Now
        };

        await _repository.AddAsync(audit);
        await _repository.SaveChangesAsync();
    }
}