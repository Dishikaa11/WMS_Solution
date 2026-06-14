using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces;

public interface ILeaveRepository
{
    Task<List<Leave>> GetAllAsync();

    Task<Leave?> GetByIdAsync(int id);

    Task AddAsync(Leave leave);

    Task UpdateAsync(Leave leave);

    Task DeleteAsync(Leave leave);

    Task SaveChangesAsync();
}