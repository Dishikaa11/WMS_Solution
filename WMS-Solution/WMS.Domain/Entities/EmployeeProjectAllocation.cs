using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Domain.Entities;

public class EmployeeProjectAllocation
{
    public int AllocationId { get; set; }

    public int EmpId { get; set; }

    public Employee? Employee { get; set; }

    public int ProjectId { get; set; }

    public Project? Project { get; set; }

    public DateTime AssignedOn { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public string Status { get; set; } = "Active";

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}