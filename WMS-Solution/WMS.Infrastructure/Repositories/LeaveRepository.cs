using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class LeaveRepository : ILeaveRepository
{
    private readonly WmsDbContext _context;

    public LeaveRepository(WmsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Leave>> GetAllAsync()
    {
        return await _context.Leaves.ToListAsync();
    }

    public async Task<Leave?> GetByIdAsync(int id)
    {
        return await _context.Leaves
            .FirstOrDefaultAsync(x => x.LeaveId == id);
    }

    public async Task AddAsync(Leave leave)
    {
        await _context.Leaves.AddAsync(leave);
    }

    public async Task UpdateAsync(Leave leave)
    {
        _context.Leaves.Update(leave);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Leave leave)
    {
        _context.Leaves.Remove(leave);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}