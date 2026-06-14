namespace WMS.Application.DTOs;

public class AnnouncementSummaryDto
{
    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }
}