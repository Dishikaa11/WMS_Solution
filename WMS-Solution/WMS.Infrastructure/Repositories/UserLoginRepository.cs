using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class UserLoginRepository : IUserLoginRepository
{
    private readonly WmsDbContext _context;

    public UserLoginRepository(WmsDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserLogin>> GetAllAsync()
    {
        return await _context.UserLogin
            .Include(x => x.Role)
            .Include(x => x.Employee)
            .ToListAsync();
    }

    public async Task<UserLogin?> GetByIdAsync(int id)
    {
        return await _context.UserLogin
            .Include(x => x.Role)
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.UserId == id);
    }

    public async Task<UserLogin?> GetByUsernameAsync(string username)
    {
        return await _context.UserLogin
            .Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task AddAsync(UserLogin user)
    {
        await _context.UserLogin.AddAsync(user);
    }

    public async Task UpdateAsync(UserLogin user)
    {
        _context.UserLogin.Update(user);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(UserLogin user)
    {
        _context.UserLogin.Remove(user);
        await Task.CompletedTask;
    }

    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}