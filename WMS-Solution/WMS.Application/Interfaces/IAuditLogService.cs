using WMS.Application.DTOs;
using WMS.Domain.Entities;

namespace WMS.Application.Interfaces;

public interface IAuditLogService
{
    Task<List<AuditLog>> GetAllAsync();

    Task<AuditLog?> GetByIdAsync(int id);

    Task AddAsync(AuditLogDto dto);


}