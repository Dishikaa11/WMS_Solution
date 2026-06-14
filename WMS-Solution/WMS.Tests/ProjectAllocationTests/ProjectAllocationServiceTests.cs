using Moq;
using Xunit;
using WMS.Application.DTOs;
using WMS.Application.Services;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Tests.EmployeeProjectAllocationTests;

public class EmployeeProjectAllocationServiceTests
{
    private readonly Mock<IEmployeeProjectAllocationRepository> _repoMock;
    private readonly EmployeeProjectAllocationService _service;

    public EmployeeProjectAllocationServiceTests()
    {
        _repoMock =
            new Mock<IEmployeeProjectAllocationRepository>();

        _service =
            new EmployeeProjectAllocationService(
                _repoMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllocations()
    {
        var allocations =
            new List<EmployeeProjectAllocation>
            {
                new EmployeeProjectAllocation
                {
                    AllocationId = 1,
                    EmpId = 1,
                    ProjectId = 1
                }
            };

        _repoMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(allocations);

        var result =
            await _service.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenAllocationExists_ShouldReturnAllocation()
    {
        var allocation =
            new EmployeeProjectAllocation
            {
                AllocationId = 1
            };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(allocation);

        var result =
            await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result!.AllocationId);
    }

    [Fact]
    public async Task GetByIdAsync_WhenAllocationNotFound_ShouldReturnNull()
    {
        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(
                (EmployeeProjectAllocation?)null);

        var result =
            await _service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldCreateAllocation()
    {
        var dto =
            new EmployeeProjectAllocationDto
            {
                EmpId = 1,
                ProjectId = 1,
                CreatedBy = "Admin"
            };

        await _service.AddAsync(dto);

        _repoMock.Verify(
            x => x.AddAsync(
                It.IsAny<EmployeeProjectAllocation>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenAllocationExists_ShouldDeleteAllocation()
    {
        var allocation =
            new EmployeeProjectAllocation
            {
                AllocationId = 1
            };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(allocation);

        await _service.DeleteAsync(1);

        _repoMock.Verify(
            x => x.DeleteAsync(
                It.IsAny<EmployeeProjectAllocation>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }
}