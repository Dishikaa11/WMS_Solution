using WMS.Application.DTOs;

namespace WMS.Application.Interfaces;

public interface IUserLoginService
{
    Task<List<UserLoginDto>> GetAllAsync();

    Task<UserLoginDto?> GetByIdAsync(int id);

    Task AddAsync(UserLoginDto dto);

    Task UpdateAsync(int id, UserLoginDto dto);

    Task DeleteAsync(int id);
}