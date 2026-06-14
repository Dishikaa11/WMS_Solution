using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly WmsDbContext _context;

    public RoleRepository(WmsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Role>> GetAllAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(x => x.RoleId == id);
    }

    public async Task AddAsync(Role role)
    {
        await _context.Roles.AddAsync(role);
    }

    public async Task UpdateAsync(Role role)
    {
        _context.Roles.Update(role);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Role role)
    {
        _context.Roles.Remove(role);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}