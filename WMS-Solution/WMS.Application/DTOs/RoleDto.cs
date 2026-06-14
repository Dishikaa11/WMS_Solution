using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Application.DTOs;

public class RoleDto
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = string.Empty;

    public string? Description { get; set; }
}