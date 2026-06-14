using Moq;
using Xunit;
using WMS.Application.DTOs;
using WMS.Application.Services;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Tests.ProjectTests;

public class ProjectServiceTests
{
    private readonly Mock<IProjectRepository> _repoMock;
    private readonly ProjectService _service;

    public ProjectServiceTests()
    {
        _repoMock = new Mock<IProjectRepository>();

        _service =
            new ProjectService(
                _repoMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnProjects()
    {
        var projects = new List<Project>
        {
            new Project
            {
                ProjectId = 1,
                ProjectName = "WMS"
            }
        };

        _repoMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(projects);

        var result =
            await _service.GetAllAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetByIdAsync_WhenProjectExists_ShouldReturnProject()
    {
        var project = new Project
        {
            ProjectId = 1,
            ProjectName = "WMS"
        };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(project);

        var result =
            await _service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("WMS", result!.ProjectName);
    }

    [Fact]
    public async Task GetByIdAsync_WhenProjectNotFound_ShouldReturnNull()
    {
        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync((Project?)null);

        var result =
            await _service.GetByIdAsync(1);

        Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_ShouldCreateProject()
    {
        var dto = new ProjectDto
        {
            ProjectName = "WMS",
            ClientId = 1
        };

        await _service.AddAsync(dto);

        _repoMock.Verify(
            x => x.AddAsync(It.IsAny<Project>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_WhenProjectExists_ShouldDeleteProject()
    {
        var project = new Project
        {
            ProjectId = 1
        };

        _repoMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(project);

        await _service.DeleteAsync(1);

        _repoMock.Verify(
            x => x.DeleteAsync(It.IsAny<Project>()),
            Times.Once);

        _repoMock.Verify(
            x => x.SaveChangesAsync(),
            Times.Once);
    }
}