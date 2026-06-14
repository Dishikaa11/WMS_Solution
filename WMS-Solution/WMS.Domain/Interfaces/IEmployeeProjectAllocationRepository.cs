using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces;

public interface IEmployeeProjectAllocationRepository
{
    Task<List<EmployeeProjectAllocation>> GetAllAsync();

    Task<EmployeeProjectAllocation?> GetByIdAsync(int id);

    Task AddAsync(EmployeeProjectAllocation allocation);

    Task UpdateAsync(EmployeeProjectAllocation allocation);

    Task DeleteAsync(EmployeeProjectAllocation allocation);

    Task SaveChangesAsync();
}   