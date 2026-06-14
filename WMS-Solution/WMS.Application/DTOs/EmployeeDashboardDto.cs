namespace WMS.Application.DTOs;

public class EmployeeDashboardDto
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public DateTime DOJ { get; set; }

    public int PresentDays { get; set; }

    public int AbsentDays { get; set; }

    public double AttendancePercentage { get; set; }

    public int AppliedLeaves { get; set; }

    public int ApprovedLeaves { get; set; }

    public int RejectedLeaves { get; set; }

    public int PendingLeaves { get; set; }

    public List<AttendanceSummaryDto> RecentAttendance { get; set; }
    = new();

    public List<LeaveSummaryDto> RecentLeaves { get; set; }
        = new();

    public List<ProjectSummaryDto> Projects { get; set; }
    = new();

    public List<AnnouncementSummaryDto> Announcements { get; set; }
    = new();
}