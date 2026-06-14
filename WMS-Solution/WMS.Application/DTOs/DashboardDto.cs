using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Application.DTOs;
public class DashboardDto
{
    public int TotalEmployees { get; set; }

    public int TotalDepartments { get; set; }

    public int TotalProjects { get; set; }

    public int ActiveProjects { get; set; }

    public int TotalClients { get; set; }

    public int PendingLeaves { get; set; }

    public int TodayAttendance { get; set; }
}