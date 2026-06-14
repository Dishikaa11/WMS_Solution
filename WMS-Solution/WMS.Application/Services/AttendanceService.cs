using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class AttendanceService : IAttendanceService
{
    private readonly IAttendanceRepository _attendanceRepository;


    public AttendanceService(IAttendanceRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    public async Task<List<AttendanceDto>> GetAllAsync()
    {
        var attendances = await _attendanceRepository.GetAllAsync();

        return attendances.Select(a => new AttendanceDto
        {
            AttendanceId = a.AttendanceId,
            EmpId = a.EmpId,
            CheckIn = a.CheckIn,
            CheckOut = a.CheckOut,
            TotalHours = a.TotalHours,
            WorkMode = a.WorkMode,
            AttendanceDate = a.AttendanceDate
        }).ToList();
    }

    public async Task<AttendanceDto?> GetByIdAsync(int id)
    {
        var attendance = await _attendanceRepository.GetByIdAsync(id);

        if (attendance == null)
            return null;

        return new AttendanceDto
        {
            AttendanceId = attendance.AttendanceId,
            EmpId = attendance.EmpId,
            CheckIn = attendance.CheckIn,
            CheckOut = attendance.CheckOut,
            TotalHours = attendance.TotalHours,
            WorkMode = attendance.WorkMode,
            AttendanceDate = attendance.AttendanceDate
        };
    }

    public async Task AddAsync(AttendanceDto dto)
    {
        var attendance = new Attendance
        {
            EmpId = dto.EmpId,
            CheckIn = dto.CheckIn,
            CheckOut = dto.CheckOut,
            TotalHours = dto.CheckOut.HasValue
    ? (dto.CheckOut.Value - dto.CheckIn).TotalHours
    : 0,
            WorkMode = dto.WorkMode,
            AttendanceDate = dto.AttendanceDate
        };

        await _attendanceRepository.AddAsync(attendance);
        await _attendanceRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, AttendanceDto dto)
    {
        var attendance = await _attendanceRepository.GetByIdAsync(id);

        if (attendance == null)
            throw new Exception("Attendance not found");

        attendance.EmpId = dto.EmpId;
        attendance.CheckIn = dto.CheckIn;
        attendance.CheckOut = dto.CheckOut;
        attendance.TotalHours =
    attendance.CheckOut.HasValue
    ? (attendance.CheckOut.Value - attendance.CheckIn).TotalHours
    : 0;
        attendance.WorkMode = dto.WorkMode;
        attendance.AttendanceDate = dto.AttendanceDate;

        await _attendanceRepository.UpdateAsync(attendance);
        await _attendanceRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var attendance = await _attendanceRepository.GetByIdAsync(id);

        if (attendance == null)
            throw new Exception("Attendance not found");

        await _attendanceRepository.DeleteAsync(attendance);
        await _attendanceRepository.SaveChangesAsync();
    }
}