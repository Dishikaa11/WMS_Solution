using WMS.Application.DTOs;

namespace WMS.Application.Interfaces;

public interface ILeaveService
{
    Task<List<LeaveDto>> GetAllAsync();

    Task<LeaveDto?> GetByIdAsync(int id);

    Task AddAsync(LeaveDto dto);

    Task UpdateAsync(int id, LeaveDto dto);

    Task DeleteAsync(int id);
    Task ApproveAsync(int leaveId, int managerId);
    Task RejectAsync(int leaveId, int managerId);
    Task CancelAsync(int leaveId);
   //Task<List<LeaveDto>>GetMyLeavesAsync(string username);
}