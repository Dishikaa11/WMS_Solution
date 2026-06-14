using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class UserLoginService : IUserLoginService
{
    private readonly IUserLoginRepository _repository;

    public UserLoginService(IUserLoginRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UserLoginDto>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();

        return users.Select(x => new UserLoginDto
        {
            UserId = x.UserId,
            Username = x.Username,
            PasswordHash = x.PasswordHash,
            EmployeeId = x.EmployeeId,
            RoleId = x.RoleId,
            LastLogin = x.LastLogin
        }).ToList();
    }

    public async Task<UserLoginDto?> GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
            return null;

        return new UserLoginDto
        {
            UserId = user.UserId,
            Username = user.Username,
            PasswordHash = user.PasswordHash,
            EmployeeId = user.EmployeeId,
            RoleId = user.RoleId,
            LastLogin = user.LastLogin
        };
    }

    public async Task AddAsync(UserLoginDto dto)
    {
        var user = new UserLogin
        {
            Username = dto.Username,
            PasswordHash = dto.PasswordHash,
            EmployeeId = dto.EmployeeId,
            RoleId = dto.RoleId
        };

        await _repository.AddAsync(user);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UserLoginDto dto)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
            throw new Exception("User not found");

        user.Username = dto.Username;
        user.PasswordHash = dto.PasswordHash;
        user.EmployeeId = dto.EmployeeId;
        user.RoleId = dto.RoleId;

        await _repository.UpdateAsync(user);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);

        if (user == null)
            throw new Exception("User not found");

        await _repository.DeleteAsync(user);
        await _repository.SaveChangesAsync();
    }
}