using WMS.Application.DTOs;

namespace WMS.Application.Interfaces;

public interface IRoleService
{
    Task<List<RoleDto>> GetAllAsync();

    Task<RoleDto?> GetByIdAsync(int id);

    Task AddAsync(RoleDto dto);

    Task UpdateAsync(int id, RoleDto dto);

    Task DeleteAsync(int id);
}