using System;
using System.Collections.Generic;
using System.Text;

using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync();

    Task<Employee?> GetByIdAsync(int id);

    Task AddAsync(Employee employee);

    Task UpdateAsync(Employee employee);

    Task DeleteAsync(Employee employee);
    Task<List<Employee>> SearchByNameAsync(string name);

    Task<List<Employee>> GetByDepartmentAsync(int departmentId);

    Task<List<Employee>> GetByRoleAsync(int roleId);

    Task SaveChangesAsync();
}