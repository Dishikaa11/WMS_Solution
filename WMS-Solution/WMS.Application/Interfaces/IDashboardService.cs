using WMS.Application.DTOs;

namespace WMS.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardDto> GetSummaryAsync();

    Task<EmployeeDashboardDto> GetEmployeeDashboardAsync(int employeeId);
}