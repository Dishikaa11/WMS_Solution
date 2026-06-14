using System;
using System.Collections.Generic;
using System.Text;

namespace WMS.Application.DTOs;

public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public DateTime? LastLogin { get; set; }

    public bool IsPasswordChanged { get; set; }
    
}
