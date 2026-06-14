using WMS.Application.DTOs;

namespace WMS.Application.Interfaces;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetAllAsync();

    Task<EmployeeDto?> GetByIdAsync(int id);

    Task<EmployeeDto> CreateAsync(EmployeeDto dto);

    Task<bool> UpdateAsync(int id, EmployeeDto dto);

    Task<bool> DeleteAsync(int id);

    Task<List<EmployeeDto>> SearchByNameAsync(string name);

    Task<List<EmployeeDto>> GetByDepartmentAsync(int departmentId);

    Task<List<EmployeeDto>> GetByRoleAsync(int roleId);
}