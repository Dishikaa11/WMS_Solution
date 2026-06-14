using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces;

public interface IUserLoginRepository
{
    Task<List<UserLogin>> GetAllAsync();

    Task<UserLogin?> GetByIdAsync(int id);

    Task<UserLogin?> GetByUsernameAsync(string username);

    Task AddAsync(UserLogin user);

    Task UpdateAsync(UserLogin user);

    Task DeleteAsync(UserLogin user);
    
    Task SaveChangesAsync();
}