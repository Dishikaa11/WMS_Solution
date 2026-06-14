using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class EmployeeProjectAllocationRepository
    : IEmployeeProjectAllocationRepository
{
    private readonly WmsDbContext _context;

    public EmployeeProjectAllocationRepository(
        WmsDbContext context)
    {
        _context = context;
    }

    public async Task<List<EmployeeProjectAllocation>> GetAllAsync()
    {
        return await _context.EmployeeProjectAllocations
            .Include(x => x.Employee)
            .Include(x => x.Project)
            .ToListAsync();
    }

    public async Task<EmployeeProjectAllocation?> GetByIdAsync(int id)
    {
        return await _context.EmployeeProjectAllocations
            .Include(x => x.Employee)
            .Include(x => x.Project)
            .FirstOrDefaultAsync(x => x.AllocationId == id);
    }

    public async Task AddAsync(
        EmployeeProjectAllocation allocation)
    {
        await _context.EmployeeProjectAllocations
            .AddAsync(allocation);
    }

    public async Task UpdateAsync(
        EmployeeProjectAllocation allocation)
    {
        _context.EmployeeProjectAllocations.Update(allocation);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(
        EmployeeProjectAllocation allocation)
    {
        _context.EmployeeProjectAllocations.Remove(allocation);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}