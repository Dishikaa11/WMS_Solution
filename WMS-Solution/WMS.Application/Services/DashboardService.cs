using Microsoft.EntityFrameworkCore;
using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Infrastructure.Data;


namespace WMS.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly WmsDbContext _context;

    public DashboardService(WmsDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> GetSummaryAsync()
    {
        return new DashboardDto
        {
            TotalEmployees =
                await _context.Employees.CountAsync(),

            TotalDepartments =
                await _context.Departments.CountAsync(),

            TotalProjects =
                await _context.Projects.CountAsync(),

            ActiveProjects =
                await _context.Projects
                    .CountAsync(x => x.Status == "Active"),

            TotalClients =
                await _context.Clients.CountAsync(),

            PendingLeaves =
                await _context.Leaves
                    .CountAsync(x => x.Status == "Pending"),

            TodayAttendance =
                await _context.Attendances
                    .CountAsync(x =>
                        x.AttendanceDate ==
                        DateOnly.FromDateTime(DateTime.Today))
        };
    }

    public async Task<EmployeeDashboardDto>
GetEmployeeDashboardAsync(int employeeId)
    {
        var employee =
            await _context.Employees
            .Include(x => x.Department)
            .Include(x => x.Role)
            .FirstOrDefaultAsync(
                x => x.EmployeeId == employeeId);

        if (employee == null)
            throw new Exception("Employee not found");

        var present =
            await _context.Attendances
            .CountAsync(x =>
                x.EmpId == employeeId &&
                x.Status == "Present");

        var absent =
            await _context.Attendances
            .CountAsync(x =>
                x.EmpId == employeeId &&
                x.Status == "Absent");

        var totalAttendance =
            present + absent;

        var attendancePercent =
            totalAttendance == 0
            ? 0
            : (double)present /
              totalAttendance * 100;

        return new EmployeeDashboardDto
        {
            Name =
                employee.FirstName + " "
                + employee.LastName,

            Email = employee.Email,

            Department =
                employee.Department?.DepartmentName
                ?? "",

            Role =
                employee.Role?.RoleName
                ?? "",

            DOJ = employee.DOJ,

            PresentDays = present,

            AbsentDays = absent,

            AttendancePercentage =
                Math.Round(
                    attendancePercent,
                    2),

            AppliedLeaves =
                await _context.Leaves
                .CountAsync(
                    x => x.EmpId ==
                         employeeId),

            ApprovedLeaves =
                await _context.Leaves
                .CountAsync(
                    x => x.EmpId ==
                         employeeId &&
                         x.Status ==
                         "Approved"),

            RejectedLeaves =
                await _context.Leaves
                .CountAsync(
                    x => x.EmpId ==
                         employeeId &&
                         x.Status ==
                         "Rejected"),

            PendingLeaves =
                await _context.Leaves
                .CountAsync(
                    x => x.EmpId ==
                         employeeId &&
                         x.Status ==
                         "Pending"),

            RecentAttendance =
                await _context.Attendances
                    .Where(x => x.EmpId == employeeId)
                    .OrderByDescending(x => x.AttendanceDate)
                    .Take(5)
                    .Select(x => new AttendanceSummaryDto
                    {
                        AttendanceDate = x.AttendanceDate,
                        CheckIn = x.CheckIn,
                        CheckOut = x.CheckOut,
                        WorkMode = x.WorkMode
                    })
                    .ToListAsync(),

            RecentLeaves =
                await _context.Leaves
                    .Where(x => x.EmpId == employeeId)
                    .OrderByDescending(x => x.AppliedOn)
                    .Take(5)
                    .Select(x => new LeaveSummaryDto
                    {
                        LeaveType = x.LeaveType,
                        FromDate = x.FromDate,
                        ToDate = x.ToDate,
                        Status = x.Status
                    })
                    .ToListAsync(),

            Projects =
                await _context.EmployeeProjectAllocations
                    .Include(x => x.Project)
                    .Where(x => x.EmpId == employeeId)
                    .Select(x => new ProjectSummaryDto
                    {
                        ProjectName =
                            x.Project != null
                            ? x.Project.ProjectName
                            : "",

                        AssignedOn = x.AssignedOn,

                        Status = x.Status
                    })
                    .ToListAsync(),

            Announcements =
                await _context.Announcements
                    .Where(x => x.IsActive)
                    .OrderByDescending(x => x.CreatedOn)
                    .Take(5)
                    .Select(x => new AnnouncementSummaryDto
                    {
                        Title = x.Title,
                        Message = x.Message,
                        CreatedOn = x.CreatedOn
                    })
                    .ToListAsync(),
        };
    }
}