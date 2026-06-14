namespace WMS.Application.DTOs;

public class LeaveSummaryDto
{
    public string LeaveType { get; set; } = string.Empty;

    public DateTime FromDate { get; set; }

    public DateTime ToDate { get; set; }

    public string Status { get; set; } = string.Empty;
}