namespace WMS.Application.DTOs;

public class AttendanceSummaryDto
{
    public DateOnly AttendanceDate { get; set; }

    public DateTime CheckIn { get; set; }

    public DateTime? CheckOut { get; set; }

    public string? WorkMode { get; set; }
}