using WMS.Application.DTOs;

namespace WMS.Application.Interfaces;

public interface IClientService
{
    Task<List<ClientDto>> GetAllAsync();

    Task<ClientDto?> GetByIdAsync(int id);

    Task AddAsync(ClientDto dto);

    Task UpdateAsync(int id, ClientDto dto);

    Task DeleteAsync(int id);
}