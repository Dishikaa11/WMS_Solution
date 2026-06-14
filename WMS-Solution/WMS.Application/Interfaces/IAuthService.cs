using WMS.Application.DTOs;

public interface IAuthService
{
    Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto);
    Task<bool> ChangePasswordAsync(
    string username,
    ChangePasswordDto dto);
}