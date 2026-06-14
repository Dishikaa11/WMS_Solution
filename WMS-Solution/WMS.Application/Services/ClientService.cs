using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<List<ClientDto>> GetAllAsync()
    {
        var clients = await _clientRepository.GetAllAsync();

        return clients.Select(c => new ClientDto
        {
            ClientId = c.ClientId,
            ClientName = c.ClientName,
            ClientAddress = c.ClientAddress,
            ClientPhoneNumber = c.ClientPhoneNumber,
            ClientLocation = c.ClientLocation,
            Status = c.Status
        }).ToList();
    }

    public async Task<ClientDto?> GetByIdAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);

        if (client == null)
            return null;

        return new ClientDto
        {
            ClientId = client.ClientId,
            ClientName = client.ClientName,
            ClientAddress = client.ClientAddress,
            ClientPhoneNumber = client.ClientPhoneNumber,
            ClientLocation = client.ClientLocation,
            Status = client.Status
        };
    }

    public async Task AddAsync(ClientDto dto)
    {
        var client = new Client
        {
            ClientName = dto.ClientName,
            ClientAddress = dto.ClientAddress,
            ClientPhoneNumber = dto.ClientPhoneNumber,
            ClientLocation = dto.ClientLocation,
            Status = true
        };

        await _clientRepository.AddAsync(client);
        await _clientRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, ClientDto dto)
    {
        var client = await _clientRepository.GetByIdAsync(id);

        if (client == null)
            throw new Exception("Client not found");

        client.ClientName = dto.ClientName;
        client.ClientAddress = dto.ClientAddress;
        client.ClientPhoneNumber = dto.ClientPhoneNumber;
        client.ClientLocation = dto.ClientLocation;
        client.Status = dto.Status;

        await _clientRepository.UpdateAsync(client);
        await _clientRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);

        if (client == null)
            throw new Exception("Client not found");

        await _clientRepository.DeleteAsync(client);
        await _clientRepository.SaveChangesAsync();
    }
}