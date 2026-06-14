using Moq;
using Xunit;
using WMS.Application.DTOs;
using WMS.Application.Services;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Tests.UserLoginTests;

public class UserLoginServiceTests
{
    private readonly Mock<IUserLoginRepository> _repoMock;
    private readonly UserLoginService _service;

    public UserLoginServiceTests()
    {
        _repoMock = new Mock<IUserLoginRepository>();

        _service =
            new UserLoginService(
                _repoMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnUsers()
    {
        var users = new List<UserLogin>
        {
            new UserLogin
            {
                UserId = 1,
                Username = "admin"
            }
        };

        _repoMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(users);

        var result =
            await _service.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenUserExists_ShouldReturnUser()
    {
        var user = new UserLogin
        {
            UserId = 1,
            Username = "admin"
        };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(user);

        var result =
            await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("admin", result!.Username);
    }

    [Fact]
    public async Task GetByIdAsync_WhenUserNotFound_ShouldReturnNull()
    {
        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync((UserLogin?)null);

        var result =
            await _service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldCreateUser()
    {
        var dto = new UserLoginDto
        {
            Username = "employee1",
            PasswordHash = "123456",
            EmployeeId = 1,
            RoleId = 3
        };

        await _service.AddAsync(dto);

        _repoMock.Verify(
            x => x.AddAsync(It.IsAny<UserLogin>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenUserExists_ShouldDeleteUser()
    {
        var user = new UserLogin
        {
            UserId = 1,
            Username = "employee1"
        };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(user);

        await _service.DeleteAsync(1);

        _repoMock.Verify(
            x => x.DeleteAsync(It.IsAny<UserLogin>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }
}