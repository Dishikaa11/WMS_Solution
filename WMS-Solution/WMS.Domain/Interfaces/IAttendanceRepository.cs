using WMS.Domain.Entities;

namespace WMS.Domain.Interfaces;

public interface IAttendanceRepository
{
    Task<List<Attendance>> GetAllAsync();

    Task<Attendance?> GetByIdAsync(int id);

    Task AddAsync(Attendance attendance);

    Task UpdateAsync(Attendance attendance);

    Task DeleteAsync(Attendance attendance);

    Task SaveChangesAsync();
}