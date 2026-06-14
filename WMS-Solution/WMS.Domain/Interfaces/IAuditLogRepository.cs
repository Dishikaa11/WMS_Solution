using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces;

public interface IAuditLogRepository
{
    Task<List<AuditLog>> GetAllAsync();

    Task<AuditLog?> GetByIdAsync(int id);

    Task AddAsync(AuditLog auditLog);

    Task SaveChangesAsync();
}