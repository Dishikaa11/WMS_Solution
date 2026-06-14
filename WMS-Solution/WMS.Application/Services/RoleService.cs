using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;

    public RoleService(IRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<RoleDto>> GetAllAsync()
    {
        var roles = await _repository.GetAllAsync();

        return roles.Select(r => new RoleDto
        {
            RoleId = r.RoleId,
            RoleName = r.RoleName,
            Description = r.Description
        }).ToList();
    }

    public async Task<RoleDto?> GetByIdAsync(int id)
    {
        var role = await _repository.GetByIdAsync(id);

        if (role == null)
            return null;

        return new RoleDto
        {
            RoleId = role.RoleId,
            RoleName = role.RoleName,
            Description = role.Description
        };
    }

    public async Task AddAsync(RoleDto dto)
    {
        var role = new Role
        {
            RoleName = dto.RoleName,
            Description = dto.Description
        };

        await _repository.AddAsync(role);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, RoleDto dto)
    {
        var role = await _repository.GetByIdAsync(id);

        if (role == null)
            throw new Exception("Role not found");

        role.RoleName = dto.RoleName;
        role.Description = dto.Description;

        await _repository.UpdateAsync(role);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var role = await _repository.GetByIdAsync(id);

        if (role == null)
            throw new Exception("Role not found");

        await _repository.DeleteAsync(role);
        await _repository.SaveChangesAsync();
    }
}