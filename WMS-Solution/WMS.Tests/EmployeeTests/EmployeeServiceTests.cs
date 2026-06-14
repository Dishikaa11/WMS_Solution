using Moq;
using Xunit;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Application.Services;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Tests;

public class EmployeeServiceTests
{
    private readonly Mock<IEmployeeRepository> _repoMock;
    private readonly Mock<IAuditLogService> _auditMock;
    private readonly EmployeeService _service;

    public EmployeeServiceTests()
    {
        _repoMock = new Mock<IEmployeeRepository>();
        _auditMock = new Mock<IAuditLogService>();

        _service =
            new EmployeeService(
                _repoMock.Object,
                _auditMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnEmployees()
    {
        var employees = new List<Employee>
        {
            new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Smith",
                Email = "john@test.com"
            }
        };

        _repoMock.Setup(x => x.GetAllAsync())
                 .ReturnsAsync(employees);

        var result = await _service.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenEmployeeExists_ShouldReturnEmployee()
    {
        var employee = new Employee
        {
            EmployeeId = 1,
            FirstName = "John"
        };

        _repoMock.Setup(x => x.GetByIdAsync(1))
                 .ReturnsAsync(employee);

        var result =
            await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result!.EmployeeId);
    }

    [Fact]
    public async Task GetByIdAsync_WhenEmployeeNotFound_ShouldReturnNull()
    {
        _repoMock.Setup(x => x.GetByIdAsync(1))
                 .ReturnsAsync((Employee?)null);

        var result =
            await _service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateEmployee()
    {
        var dto = new EmployeeDto
        {
            FirstName = "John",
            LastName = "Smith",
            Email = "john@test.com"
        };

        var result =
            await _service.CreateAsync(dto);

        _repoMock.Verify(
            x => x.AddAsync(It.IsAny<Employee>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenEmployeeExists_ShouldReturnTrue()
    {
        var employee = new Employee
        {
            EmployeeId = 1
        };

        _repoMock.Setup(x => x.GetByIdAsync(1))
                 .ReturnsAsync(employee);

        var result =
            await _service.DeleteAsync(1);

        Assert.True(result);

        _repoMock.Verify(
            x => x.DeleteAsync(It.IsAny<Employee>()),
            Times.Once);
    }
}