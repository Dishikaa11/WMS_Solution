using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class LeaveService : ILeaveService
{
    private readonly ILeaveRepository _leaveRepository;

    public LeaveService(ILeaveRepository leaveRepository)
    {
        _leaveRepository = leaveRepository;
    }

    public async Task<List<LeaveDto>> GetAllAsync()
    {
        var leaves = await _leaveRepository.GetAllAsync();

        return leaves.Select(l => new LeaveDto
        {
            LeaveId = l.LeaveId,
            EmpId = l.EmpId,
            LeaveType = l.LeaveType,
            Reason = l.Reason,
            FromDate = l.FromDate,
            ToDate = l.ToDate,
            Status = l.Status,
            AppliedOn = l.AppliedOn,
            ApprovedBy = l.ApprovedBy,
            ApprovedOn = l.ApprovedOn
        }).ToList();
    }

    public async Task<LeaveDto?> GetByIdAsync(int id)
    {
        var leave = await _leaveRepository.GetByIdAsync(id);

        if (leave == null)
            return null;

        return new LeaveDto
        {
            LeaveId = leave.LeaveId,
            EmpId = leave.EmpId,
            LeaveType = leave.LeaveType,
            Reason = leave.Reason,
            FromDate = leave.FromDate,
            ToDate = leave.ToDate,
            Status = leave.Status,
            AppliedOn = leave.AppliedOn,
            ApprovedBy = leave.ApprovedBy,
            ApprovedOn = leave.ApprovedOn
        };
    }

    public async Task AddAsync(LeaveDto dto)
    {
        var leave = new Leave
        {
            EmpId = dto.EmpId,
            LeaveType = dto.LeaveType,
            Reason = dto.Reason,
            FromDate = dto.FromDate,
            ToDate = dto.ToDate,

            // Backend automatically
            Status = "Pending",
            AppliedOn = DateTime.Now,
            ApprovedBy = null,
            ApprovedOn = null
        };

        await _leaveRepository.AddAsync(leave);
        await _leaveRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, LeaveDto dto)
    {
        var leave = await _leaveRepository.GetByIdAsync(id);

        if (leave == null)
            throw new Exception("Leave not found");

        leave.EmpId = dto.EmpId;
        leave.LeaveType = dto.LeaveType;
        leave.Reason = dto.Reason;
        leave.FromDate = dto.FromDate;
        leave.ToDate = dto.ToDate;

        await _leaveRepository.UpdateAsync(leave);
        await _leaveRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var leave = await _leaveRepository.GetByIdAsync(id);

        if (leave == null)
            throw new Exception("Leave not found");

        await _leaveRepository.DeleteAsync(leave);
        await _leaveRepository.SaveChangesAsync();
    }

    public async Task ApproveAsync(int leaveId, int managerId)
    {
        var leave = await _leaveRepository.GetByIdAsync(leaveId);

        if (leave == null)
            throw new Exception("Leave not found");

        leave.Status = "Approved";
        leave.ApprovedBy = managerId;
        leave.ApprovedOn = DateTime.Now;

        await _leaveRepository.UpdateAsync(leave);
        await _leaveRepository.SaveChangesAsync();
    }

    public async Task RejectAsync(int leaveId, int managerId)
    {
        var leave = await _leaveRepository.GetByIdAsync(leaveId);

        if (leave == null)
            throw new Exception("Leave not found");

        leave.Status = "Rejected";
        leave.ApprovedBy = managerId;
        leave.ApprovedOn = DateTime.Now;

        await _leaveRepository.UpdateAsync(leave);
        await _leaveRepository.SaveChangesAsync();
    }

    public async Task CancelAsync(int leaveId)
    {
        var leave = await _leaveRepository.GetByIdAsync(leaveId);

        if (leave == null)
            throw new Exception("Leave not found");

        // Optional validation
        if (leave.Status == "Approved")
            throw new Exception("Approved leave cannot be cancelled");

        leave.Status = "Cancelled";

        await _leaveRepository.UpdateAsync(leave);
        await _leaveRepository.SaveChangesAsync();
    }
}