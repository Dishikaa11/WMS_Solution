namespace WMS.Application.DTOs;

public class ProjectSummaryDto
{
    public string ProjectName { get; set; } = string.Empty;

    public DateTime AssignedOn { get; set; }

    public string Status { get; set; } = string.Empty;
}