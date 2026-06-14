using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Interfaces;

public class AuthService : IAuthService
{
    private readonly IUserLoginRepository _repository;
    private readonly IJwtService _jwtService;

    public AuthService(
        IUserLoginRepository repository,
        IJwtService jwtService)
    {
        _repository = repository;
        _jwtService = jwtService;
    }

    public async Task<LoginResponseDto?> LoginAsync(
    LoginRequestDto dto)
    {
        var user = await _repository
            .GetByUsernameAsync(dto.Username);

        if (user == null)
            return null;

        if (user.PasswordHash != dto.Password)
            return null;

        // Last Login Update
        user.LastLogin = DateTime.Now;

        await _repository.SaveChangesAsync();

        var token = _jwtService.GenerateToken(
            user.Username,
            user.Role!.RoleName,user.EmployeeId);

        return new LoginResponseDto
        {
            Token = token,
            Username = user.Username,
            Role = user.Role.RoleName,
            LastLogin = user.LastLogin,
            IsPasswordChanged =user.IsPasswordChanged
        };
    }

    public async Task<bool> ChangePasswordAsync(
    string username,
    ChangePasswordDto dto)
    {
        var user = await _repository
            .GetByUsernameAsync(username);

        if (user == null)
            return false;

        if (user.PasswordHash != dto.CurrentPassword)
            return false;

        user.PasswordHash = dto.NewPassword;

        user.IsPasswordChanged = true;

        await _repository.UpdateAsync(user);
        await _repository.SaveChangesAsync();

        return true;
    }
}