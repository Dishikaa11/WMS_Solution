using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Domain.Entities;

public class Project
{
    public int ProjectId { get; set; }

    public string ProjectName { get; set; } = string.Empty;

    public int? ClientId { get; set; }

    public Client? Client { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Status { get; set; } = "Active";

    public ICollection<EmployeeProjectAllocation> EmployeeProjectAllocations { get; set; }
    = new List<EmployeeProjectAllocation>();
}