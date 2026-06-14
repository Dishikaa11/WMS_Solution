using Moq;
using Xunit;
using WMS.Application.DTOs;
using WMS.Application.Services;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Tests.AnnouncementTests;

public class AnnouncementServiceTests
{
    private readonly Mock<IAnnouncementRepository> _repoMock;
    private readonly AnnouncementService _service;

    public AnnouncementServiceTests()
    {
        _repoMock = new Mock<IAnnouncementRepository>();

        _service =
            new AnnouncementService(
                _repoMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAnnouncements()
    {
        var announcements = new List<Announcement>
        {
            new Announcement
            {
                AnnouncementId = 1,
                Title = "Holiday Notice"
            }
        };

        _repoMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(announcements);

        var result =
            await _service.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenAnnouncementExists_ShouldReturnAnnouncement()
    {
        var announcement = new Announcement
        {
            AnnouncementId = 1,
            Title = "Holiday Notice"
        };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(announcement);

        var result =
            await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Holiday Notice", result!.Title);
    }

    [Fact]
    public async Task GetByIdAsync_WhenAnnouncementNotFound_ShouldReturnNull()
    {
        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync((Announcement?)null);

        var result =
            await _service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldCreateAnnouncement()
    {
        var dto = new AnnouncementDto
        {
            Title = "Holiday",
            Message = "Office Closed",
            CreatedBy = 1
        };

        await _service.AddAsync(dto);

        _repoMock.Verify(
            x => x.AddAsync(It.IsAny<Announcement>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenAnnouncementExists_ShouldDeleteAnnouncement()
    {
        var announcement = new Announcement
        {
            AnnouncementId = 1
        };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(announcement);

        await _service.DeleteAsync(1);

        _repoMock.Verify(
            x => x.DeleteAsync(It.IsAny<Announcement>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }
}