using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class EmployeeProjectAllocationService
    : IEmployeeProjectAllocationService
{
    private readonly IEmployeeProjectAllocationRepository
        _allocationRepository;

    public EmployeeProjectAllocationService(
        IEmployeeProjectAllocationRepository allocationRepository)
    {
        _allocationRepository = allocationRepository;
    }

    public async Task<List<EmployeeProjectAllocationDto>>
        GetAllAsync()
    {
        var allocations =
            await _allocationRepository.GetAllAsync();

        return allocations.Select(x =>
            new EmployeeProjectAllocationDto
            {
                AllocationId = x.AllocationId,
                EmpId = x.EmpId,
                ProjectId = x.ProjectId,
                AssignedOn = x.AssignedOn,
                CreateDate = x.CreateDate,
                CreatedBy = x.CreatedBy,
                Status = x.Status,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate
            }).ToList();
    }

    public async Task<EmployeeProjectAllocationDto?>
        GetByIdAsync(int id)
    {
        var allocation =
            await _allocationRepository.GetByIdAsync(id);

        if (allocation == null)
            return null;

        return new EmployeeProjectAllocationDto
        {
            AllocationId = allocation.AllocationId,
            EmpId = allocation.EmpId,
            ProjectId = allocation.ProjectId,
            AssignedOn = allocation.AssignedOn,
            CreateDate = allocation.CreateDate,
            CreatedBy = allocation.CreatedBy,
            Status = allocation.Status,
            UpdatedBy = allocation.UpdatedBy,
            UpdatedDate = allocation.UpdatedDate
        };
    }

    public async Task AddAsync(
        EmployeeProjectAllocationDto dto)
    {
        var allocation =
            new EmployeeProjectAllocation
            {
                EmpId = dto.EmpId,
                ProjectId = dto.ProjectId,
                AssignedOn = dto.AssignedOn,
                CreatedBy = dto.CreatedBy,

                CreateDate = DateTime.Now,
                Status = "Active"
            };

        await _allocationRepository.AddAsync(allocation);
        await _allocationRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(
        int id,
        EmployeeProjectAllocationDto dto)
    {
        var allocation =
            await _allocationRepository.GetByIdAsync(id);

        if (allocation == null)
            throw new Exception("Allocation not found");

        allocation.EmpId = dto.EmpId;
        allocation.ProjectId = dto.ProjectId;
        allocation.AssignedOn = dto.AssignedOn;
        allocation.Status = dto.Status;

        allocation.UpdatedBy = dto.UpdatedBy;
        allocation.UpdatedDate = DateTime.Now;

        await _allocationRepository.UpdateAsync(allocation);
        await _allocationRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var allocation =
            await _allocationRepository.GetByIdAsync(id);

        if (allocation == null)
            throw new Exception("Allocation not found");

        await _allocationRepository.DeleteAsync(allocation);
        await _allocationRepository.SaveChangesAsync();
    }
}