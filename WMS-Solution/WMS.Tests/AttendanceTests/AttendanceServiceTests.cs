using Moq;
using Xunit;
using WMS.Application.DTOs;
using WMS.Application.Services;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Tests.AttendanceTests;

public class AttendanceServiceTests
{
    private readonly Mock<IAttendanceRepository> _repoMock;
    private readonly AttendanceService _service;

    public AttendanceServiceTests()
    {
        _repoMock = new Mock<IAttendanceRepository>();

        _service =
            new AttendanceService(
                _repoMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAttendanceRecords()
    {
        var records = new List<Attendance>
        {
            new Attendance
            {
                AttendanceId = 1,
                EmpId = 1
            }
        };

        _repoMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(records);

        var result =
            await _service.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenAttendanceExists_ShouldReturnAttendance()
    {
        var attendance = new Attendance
        {
            AttendanceId = 1,
            EmpId = 1
        };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(attendance);

        var result =
            await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal(1, result!.AttendanceId);
    }

    [Fact]
    public async Task GetByIdAsync_WhenAttendanceNotFound_ShouldReturnNull()
    {
        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync((Attendance?)null);

        var result =
            await _service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldCreateAttendance()
    {
        var dto = new AttendanceDto
        {
            EmpId = 1,
            CheckIn = DateTime.Now,
            CheckOut = DateTime.Now.AddHours(8),
            WorkMode = "WFH",
            AttendanceDate =
                DateOnly.FromDateTime(DateTime.Today)
        };

        await _service.AddAsync(dto);

        _repoMock.Verify(
            x => x.AddAsync(It.IsAny<Attendance>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenAttendanceExists_ShouldDeleteAttendance()
    {
        var attendance = new Attendance
        {
            AttendanceId = 1
        };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(attendance);

        await _service.DeleteAsync(1);

        _repoMock.Verify(
            x => x.DeleteAsync(It.IsAny<Attendance>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }
}