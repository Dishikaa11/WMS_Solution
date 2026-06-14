using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace WMS.Domain.Entities;

public class Employee
{
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string Gender { get; set; } = string.Empty;

    public DateTime DOB { get; set; }

    public DateTime DOJ { get; set; }

    public int DepartmentId { get; set; }

    public Department? Department { get; set; }

    public string Status { get; set; } = "Active";

    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public DateTime? UpdatedOn { get; set; }
    public int RoleId { get; set; }

    public Role? Role { get; set; }
    public UserLogin? UserLogin { get; set; }

    public ICollection<Attendance> Attendances { get; set; }
    = new List<Attendance>();

    public ICollection<EmployeeProjectAllocation> EmployeeProjectAllocations { get; set; }
     = new List<EmployeeProjectAllocation>();
}