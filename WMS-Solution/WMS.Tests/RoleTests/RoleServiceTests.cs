using Moq;
using Xunit;
using WMS.Application.DTOs;
using WMS.Application.Services;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Tests.RoleTests;

public class RoleServiceTests
{
    private readonly Mock<IRoleRepository> _repoMock;
    private readonly RoleService _service;

    public RoleServiceTests()
    {
        _repoMock = new Mock<IRoleRepository>();

        _service =
            new RoleService(
                _repoMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnRoles()
    {
        var roles = new List<Role>
        {
            new Role
            {
                RoleId = 1,
                RoleName = "Admin"
            }
        };

        _repoMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(roles);

        var result =
            await _service.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenRoleExists_ShouldReturnRole()
    {
        var role = new Role
        {
            RoleId = 1,
            RoleName = "Admin"
        };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(role);

        var result =
            await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Admin", result!.RoleName);
    }

    [Fact]
    public async Task GetByIdAsync_WhenRoleNotFound_ShouldReturnNull()
    {
        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync((Role?)null);

        var result =
            await _service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldCreateRole()
    {
        var dto = new RoleDto
        {
            RoleName = "Manager",
            Description = "Manager Role"
        };

        await _service.AddAsync(dto);

        _repoMock.Verify(
            x => x.AddAsync(It.IsAny<Role>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenRoleExists_ShouldDeleteRole()
    {
        var role = new Role
        {
            RoleId = 1
        };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(role);

        await _service.DeleteAsync(1);

        _repoMock.Verify(
            x => x.DeleteAsync(It.IsAny<Role>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }
}