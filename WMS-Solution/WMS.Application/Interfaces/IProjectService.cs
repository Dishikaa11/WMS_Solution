using WMS.Application.DTOs;

namespace WMS.Application.Interfaces;

public interface IProjectService
{
    Task<List<ProjectDto>> GetAllAsync();
    Task<ProjectDto?> GetByIdAsync(int id);
    Task AddAsync(ProjectDto dto);
    Task UpdateAsync(int id, ProjectDto dto);
    Task DeleteAsync(int id);
}