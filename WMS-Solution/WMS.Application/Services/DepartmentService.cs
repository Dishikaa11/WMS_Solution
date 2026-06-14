using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repository;

    public DepartmentService(IDepartmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<DepartmentDto>> GetAllAsync()
    {
        var departments = await _repository.GetAllAsync();

        return departments.Select(x => new DepartmentDto
        {
            DepartmentId = x.DepartmentId,
            DepartmentName = x.DepartmentName,
            Description = x.Description
        }).ToList();
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id)
    {
        var department = await _repository.GetByIdAsync(id);

        if (department == null)
            return null;

        return new DepartmentDto
        {
            DepartmentId = department.DepartmentId,
            DepartmentName = department.DepartmentName,
            Description = department.Description
        };
    }

    public async Task<DepartmentDto> CreateAsync(DepartmentDto dto)
    {
        var department = new Department
        {
            DepartmentName = dto.DepartmentName,
            Description = dto.Description,
            CreatedOn = DateTime.Now
        };

        await _repository.AddAsync(department);
        await _repository.SaveChangesAsync();

        dto.DepartmentId = department.DepartmentId;

        return dto;
    }

    public async Task<bool> UpdateAsync(int id, DepartmentDto dto)
    {
        var department = await _repository.GetByIdAsync(id);

        if (department == null)
            return false;

        department.DepartmentName = dto.DepartmentName;
        department.Description = dto.Description;

        await _repository.UpdateAsync(department);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var department = await _repository.GetByIdAsync(id);

        if (department == null)
            return false;

        await _repository.DeleteAsync(department);
        await _repository.SaveChangesAsync();

        return true;
    }
}