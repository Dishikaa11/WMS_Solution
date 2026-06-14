using Microsoft.EntityFrameworkCore;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly WmsDbContext _context;

    public ClientRepository(WmsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Client>> GetAllAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _context.Clients
            .FirstOrDefaultAsync(x => x.ClientId == id);
    }

    public async Task AddAsync(Client client)
    {
        await _context.Clients.AddAsync(client);
    }

    public async Task UpdateAsync(Client client)
    {
        _context.Clients.Update(client);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Client client)
    {
        _context.Clients.Remove(client);
        await Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}