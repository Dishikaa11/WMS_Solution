using System;
using System.Collections.Generic;
using System.Text;

using WMS.Application.DTOs;

namespace WMS.Application.Interfaces;

public interface IDepartmentService
{
    Task<List<DepartmentDto>> GetAllAsync();

    Task<DepartmentDto?> GetByIdAsync(int id);

    Task<DepartmentDto> CreateAsync(DepartmentDto dto);
    Task<bool> UpdateAsync(int id, DepartmentDto dto);

    Task<bool> DeleteAsync(int id);
}