using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<List<ProjectDto>> GetAllAsync()
    {
        var projects = await _projectRepository.GetAllAsync();

        return projects.Select(p => new ProjectDto
        {
            ProjectId = p.ProjectId,
            ProjectName = p.ProjectName,
            ClientId = p.ClientId,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            Status = p.Status
        }).ToList();
    }

    public async Task<ProjectDto?> GetByIdAsync(int id)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null)
            return null;

        return new ProjectDto
        {
            ProjectId = project.ProjectId,
            ProjectName = project.ProjectName,
            ClientId = project.ClientId,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            Status = project.Status
        };
    }

    public async Task AddAsync(ProjectDto dto)
    {
        var project = new Project
        {
            ProjectName = dto.ProjectName,
            ClientId = dto.ClientId,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Status = dto.Status
        };

        await _projectRepository.AddAsync(project);
        await _projectRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, ProjectDto dto)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null)
            throw new Exception("Project not found");

        project.ProjectName = dto.ProjectName;
        project.ClientId = dto.ClientId;
        project.StartDate = dto.StartDate;
        project.EndDate = dto.EndDate;
        project.Status = dto.Status;

        await _projectRepository.UpdateAsync(project);
        await _projectRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var project = await _projectRepository.GetByIdAsync(id);

        if (project == null)
            throw new Exception("Project not found");

        await _projectRepository.DeleteAsync(project);
        await _projectRepository.SaveChangesAsync();
    }
}