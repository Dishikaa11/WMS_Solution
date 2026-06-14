using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class AttendanceRepository : IAttendanceRepository
{
    private readonly WmsDbContext _context;

    public AttendanceRepository(WmsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Attendance>> GetAllAsync()
    {
        return await _context.Attendances
            .Include(x => x.Employee)
            .ToListAsync();
    }

    public async Task<Attendance?> GetByIdAsync(int id)
    {
        return await _context.Attendances
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.AttendanceId == id);
    }

    public async Task AddAsync(Attendance attendance)
    {
        await _context.Attendances.AddAsync(attendance);
    }

    public async Task UpdateAsync(Attendance attendance)
    {
        _context.Attendances.Update(attendance);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Attendance attendance)
    {
        _context.Attendances.Remove(attendance);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}