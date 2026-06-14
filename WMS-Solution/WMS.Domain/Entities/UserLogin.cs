using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;

namespace WMS.Domain.Entities;

public class UserLogin
{
    [Key]
    public int UserId { get; set; }

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public int EmployeeId { get; set; }

    public Employee? Employee { get; set; }

    public int RoleId { get; set; }

    public Role? Role { get; set; }

    public DateTime? LastLogin { get; set; }
    public bool IsPasswordChanged { get; set; } = false;
}