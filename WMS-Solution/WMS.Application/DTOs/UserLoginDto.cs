namespace WMS.Application.DTOs;

public class UserLoginDto
{
    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public int EmployeeId { get; set; }

    public int RoleId { get; set; }

    public DateTime? LastLogin { get; set; }
    public bool IsPasswordChanged { get; set; }
}