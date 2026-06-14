using Moq;
using Xunit;
using WMS.Application.DTOs;
using WMS.Application.Services;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Tests.ClientTests;

public class ClientServiceTests
{
    private readonly Mock<IClientRepository> _repoMock;
    private readonly ClientService _service;

    public ClientServiceTests()
    {
        _repoMock = new Mock<IClientRepository>();

        _service =
            new ClientService(
                _repoMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnClients()
    {
        var clients = new List<Client>
        {
            new Client
            {
                ClientId = 1,
                ClientName = "Microsoft"
            }
        };

        _repoMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(clients);

        var result =
            await _service.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenClientExists_ShouldReturnClient()
    {
        var client = new Client
        {
            ClientId = 1,
            ClientName = "Microsoft"
        };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(client);

        var result =
            await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Microsoft", result!.ClientName);
    }

    [Fact]
    public async Task GetByIdAsync_WhenClientNotFound_ShouldReturnNull()
    {
        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync((Client?)null);

        var result =
            await _service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldCreateClient()
    {
        var dto = new ClientDto
        {
            ClientName = "Google",
            ClientAddress = "USA"
        };

        await _service.AddAsync(dto);

        _repoMock.Verify(
            x => x.AddAsync(It.IsAny<Client>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenClientExists_ShouldDeleteClient()
    {
        var client = new Client
        {
            ClientId = 1
        };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(client);

        await _service.DeleteAsync(1);

        _repoMock.Verify(
            x => x.DeleteAsync(It.IsAny<Client>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }
}