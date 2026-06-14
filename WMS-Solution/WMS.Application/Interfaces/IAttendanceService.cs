using WMS.Application.DTOs;

namespace WMS.Application.Interfaces;

public interface IAttendanceService
{
    Task<List<AttendanceDto>> GetAllAsync();

    Task<AttendanceDto?> GetByIdAsync(int id);

    Task AddAsync(AttendanceDto dto);

    Task UpdateAsync(int id, AttendanceDto dto);

    Task DeleteAsync(int id);
    //Task<List<AttendanceDto>>GetMyAttendanceAsync(string username);
}