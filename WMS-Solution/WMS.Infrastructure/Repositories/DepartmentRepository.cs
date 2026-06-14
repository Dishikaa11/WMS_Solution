using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly WmsDbContext _context;

    public DepartmentRepository(WmsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Department>> GetAllAsync()
    {
        return await _context.Departments.ToListAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _context.Departments
            .FirstOrDefaultAsync(x => x.DepartmentId == id);
    }

    public async Task AddAsync(Department department)
    {
        await _context.Departments.AddAsync(department);
    }

    public async Task UpdateAsync(Department department)
    {
        _context.Departments.Update(department);

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Department department)
    {
        _context.Departments.Remove(department);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}