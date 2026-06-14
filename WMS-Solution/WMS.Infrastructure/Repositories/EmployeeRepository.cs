using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly WmsDbContext _context;

    public EmployeeRepository(WmsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Role)
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Role)
            .FirstOrDefaultAsync(x => x.EmployeeId == id);
    }

    public async Task AddAsync(Employee employee)
    {
        await _context.Employees.AddAsync(employee);
    }

    public async Task UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Employee employee)
    {
        _context.Employees.Remove(employee);
        await Task.CompletedTask;
    }

    public async Task<List<Employee>> SearchByNameAsync(string name)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Role)
            .Where(e =>
                e.FirstName.Contains(name) ||
                e.LastName.Contains(name))
            .ToListAsync();
    }

    public async Task<List<Employee>> GetByDepartmentAsync(int departmentId)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Role)
            .Where(e => e.DepartmentId == departmentId)
            .ToListAsync();
    }

    public async Task<List<Employee>> GetByRoleAsync(int roleId)
    {
        return await _context.Employees
            .Include(e => e.Department)
            .Include(e => e.Role)
            .Where(e => e.RoleId == roleId)
            .ToListAsync();
    }
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}