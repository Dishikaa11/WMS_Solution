using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly WmsDbContext _context;

    public AnnouncementRepository(WmsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Announcement>> GetAllAsync()
    {
        return await _context.Announcements.ToListAsync();
    }

    public async Task<Announcement?> GetByIdAsync(int id)
    {
        return await _context.Announcements
            .FirstOrDefaultAsync(x => x.AnnouncementId == id);
    }

    public async Task AddAsync(Announcement announcement)
    {
        await _context.Announcements.AddAsync(announcement);
    }

    public async Task UpdateAsync(Announcement announcement)
    {
        _context.Announcements.Update(announcement);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Announcement announcement)
    {
        _context.Announcements.Remove(announcement);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}