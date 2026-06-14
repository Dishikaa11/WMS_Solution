using Moq;
using Xunit;
using WMS.Application.DTOs;
using WMS.Application.Services;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Tests.DepartmentTests;

public class DepartmentServiceTests
{
    private readonly Mock<IDepartmentRepository> _repoMock;
    private readonly DepartmentService _service;

    public DepartmentServiceTests()
    {
        _repoMock = new Mock<IDepartmentRepository>();

        _service =
            new DepartmentService(
                _repoMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnDepartments()
    {
        var departments = new List<Department>
        {
            new Department
            {
                DepartmentId = 1,
                DepartmentName = "IT"
            }
        };

        _repoMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(departments);

        var result =
            await _service.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenDepartmentExists_ShouldReturnDepartment()
    {
        var department = new Department
        {
            DepartmentId = 1,
            DepartmentName = "IT"
        };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(department);

        var result =
            await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("IT", result!.DepartmentName);
    }

    [Fact]
    public async Task GetByIdAsync_WhenDepartmentNotFound_ShouldReturnNull()
    {
        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync((Department?)null);

        var result =
            await _service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateDepartment()
    {
        var dto = new DepartmentDto
        {
            DepartmentName = "HR",
            Description = "Human Resources"
        };

        await _service.CreateAsync(dto);

        _repoMock.Verify(
            x => x.AddAsync(It.IsAny<Department>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenDepartmentExists_ShouldReturnTrue()
    {
        var department = new Department
        {
            DepartmentId = 1
        };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(department);

        var result =
            await _service.DeleteAsync(1);

        Assert.True(result);

        _repoMock.Verify(
            x => x.DeleteAsync(It.IsAny<Department>()),
            Times.Once);
    }
}