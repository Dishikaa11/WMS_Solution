using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;
    private readonly IAuditLogService _auditLogService;

    public EmployeeService(
        IEmployeeRepository repository,
        IAuditLogService auditLogService)
    {
        _repository = repository;
        _auditLogService = auditLogService;
    }

    public async Task<List<EmployeeDto>> GetAllAsync()
    {
        var employees = await _repository.GetAllAsync();

        return employees.Select(e => new EmployeeDto
        {
            EmployeeId = e.EmployeeId,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            PhoneNumber = e.PhoneNumber,
            Gender = e.Gender,
            DOB = e.DOB,
            DOJ = e.DOJ,
            DepartmentId = e.DepartmentId,
            RoleId = e.RoleId
        }).ToList();
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee == null)
            return null;

        return new EmployeeDto
        {
            EmployeeId = employee.EmployeeId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            Gender = employee.Gender,
            DOB = employee.DOB,
            DOJ = employee.DOJ,
            DepartmentId = employee.DepartmentId,
            RoleId = employee.RoleId
        };
    }

    public async Task<EmployeeDto> CreateAsync(EmployeeDto dto)
    {
        var employee = new Employee
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Gender = dto.Gender,
            DOB = dto.DOB,
            DOJ = dto.DOJ,
            DepartmentId = dto.DepartmentId,
            RoleId = dto.RoleId,
            CreatedOn = DateTime.Now
        };

        await _repository.AddAsync(employee);
        await _repository.SaveChangesAsync();

        dto.EmployeeId = employee.EmployeeId;

        return dto;
    }

    public async Task<bool> UpdateAsync(int id, EmployeeDto dto)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee == null)
            return false;

        employee.FirstName = dto.FirstName;
        employee.LastName = dto.LastName;
        employee.Email = dto.Email;
        employee.PhoneNumber = dto.PhoneNumber;
        employee.Gender = dto.Gender;
        employee.DOB = dto.DOB;
        employee.DOJ = dto.DOJ;
        employee.DepartmentId = dto.DepartmentId;
        employee.RoleId = dto.RoleId;
        employee.UpdatedOn = DateTime.Now;

        await _repository.UpdateAsync(employee);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id);

        if (employee == null)
            return false;

        await _repository.DeleteAsync(employee);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<List<EmployeeDto>> SearchByNameAsync(string name)
    {
        var employees = await _repository.SearchByNameAsync(name);

        return employees.Select(e => new EmployeeDto
        {
            EmployeeId = e.EmployeeId,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            PhoneNumber = e.PhoneNumber,
            Gender = e.Gender,
            DOB = e.DOB,
            DOJ = e.DOJ,
            DepartmentId = e.DepartmentId,
            RoleId = e.RoleId
        }).ToList();
    }

    public async Task<List<EmployeeDto>> GetByDepartmentAsync(int departmentId)
    {
        var employees =
            await _repository.GetByDepartmentAsync(departmentId);

        return employees.Select(e => new EmployeeDto
        {
            EmployeeId = e.EmployeeId,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            PhoneNumber = e.PhoneNumber,
            Gender = e.Gender,
            DOB = e.DOB,
            DOJ = e.DOJ,
            DepartmentId = e.DepartmentId,
            RoleId = e.RoleId
        }).ToList();
    }

    public async Task<List<EmployeeDto>> GetByRoleAsync(int roleId)
    {
        var employees =
            await _repository.GetByRoleAsync(roleId);

        return employees.Select(e => new EmployeeDto
        {
            EmployeeId = e.EmployeeId,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Email = e.Email,
            PhoneNumber = e.PhoneNumber,
            Gender = e.Gender,
            DOB = e.DOB,
            DOJ = e.DOJ,
            DepartmentId = e.DepartmentId,
            RoleId = e.RoleId
        }).ToList();
    }
}