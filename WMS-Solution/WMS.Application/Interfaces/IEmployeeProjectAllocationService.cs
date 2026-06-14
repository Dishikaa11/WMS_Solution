using WMS.Application.DTOs;

namespace WMS.Application.Interfaces;

public interface IEmployeeProjectAllocationService
{
    Task<List<EmployeeProjectAllocationDto>> GetAllAsync();

    Task<EmployeeProjectAllocationDto?> GetByIdAsync(int id);

    Task AddAsync(EmployeeProjectAllocationDto dto);

    Task UpdateAsync(int id,
        EmployeeProjectAllocationDto dto);

    Task DeleteAsync(int id);
}