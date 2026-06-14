using Moq;
using Xunit;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Tests.AuthTests;

public class AuthServiceTests
{
    private readonly Mock<IUserLoginRepository> _repoMock;
    private readonly Mock<IJwtService> _jwtMock;
    private readonly AuthService _service;

    public AuthServiceTests()
    {
        _repoMock = new Mock<IUserLoginRepository>();
        _jwtMock = new Mock<IJwtService>();

        _service =
            new AuthService(
                _repoMock.Object,
                _jwtMock.Object);
    }

    [Fact]
    public async Task LoginAsync_ValidCredentials_ShouldReturnToken()
    {
        var user = new UserLogin
        {
            Username = "admin",
            PasswordHash = "123",
            EmployeeId = 1,
            Role = new Role
            {
                RoleName = "Admin"
            }
        };

        _repoMock
            .Setup(x => x.GetByUsernameAsync("admin"))
            .ReturnsAsync(user);

        _jwtMock
            .Setup(x => x.GenerateToken(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>()))
            .Returns("fake-jwt-token");

        var dto = new LoginRequestDto
        {
            Username = "admin",
            Password = "123"
        };

        var result =
            await _service.LoginAsync(dto);

        Assert.NotNull(result);
        Assert.Equal("fake-jwt-token", result!.Token);
    }

    [Fact]
    public async Task LoginAsync_InvalidUsername_ShouldReturnNull()
    {
        _repoMock
            .Setup(x => x.GetByUsernameAsync("admin"))
            .ReturnsAsync((UserLogin?)null);

        var dto = new LoginRequestDto
        {
            Username = "admin",
            Password = "123"
        };

        var result =
            await _service.LoginAsync(dto);

        Assert.Null(result);
    }

    [Fact]
    public async Task LoginAsync_InvalidPassword_ShouldReturnNull()
    {
        var user = new UserLogin
        {
            Username = "admin",
            PasswordHash = "correct"
        };

        _repoMock
            .Setup(x => x.GetByUsernameAsync("admin"))
            .ReturnsAsync(user);

        var dto = new LoginRequestDto
        {
            Username = "admin",
            Password = "wrong"
        };

        var result =
            await _service.LoginAsync(dto);

        Assert.Null(result);
    }

    [Fact]
    public async Task ChangePasswordAsync_ShouldReturnTrue()
    {
        var user = new UserLogin
        {
            Username = "admin",
            PasswordHash = "old123"
        };

        _repoMock
            .Setup(x => x.GetByUsernameAsync("admin"))
            .ReturnsAsync(user);

        var dto = new ChangePasswordDto
        {
            CurrentPassword = "old123",
            NewPassword = "new123"
        };

        var result =
            await _service.ChangePasswordAsync(
                "admin",
                dto);

        Assert.True(result);
    }

    [Fact]
    public async Task ChangePasswordAsync_WrongPassword_ShouldReturnFalse()
    {
        var user = new UserLogin
        {
            Username = "admin",
            PasswordHash = "old123"
        };

        _repoMock
            .Setup(x => x.GetByUsernameAsync("admin"))
            .ReturnsAsync(user);

        var dto = new ChangePasswordDto
        {
            CurrentPassword = "wrong",
            NewPassword = "new123"
        };

        var result =
            await _service.ChangePasswordAsync(
                "admin",
                dto);

        Assert.False(result);
    }
}