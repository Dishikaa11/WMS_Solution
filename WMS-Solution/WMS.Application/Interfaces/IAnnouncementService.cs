using WMS.Application.DTOs;

namespace WMS.Application.Interfaces;

public interface IAnnouncementService
{
    Task<List<AnnouncementDto>> GetAllAsync();

    Task<AnnouncementDto?> GetByIdAsync(int id);

    Task AddAsync(AnnouncementDto dto);

    Task UpdateAsync(int id, AnnouncementDto dto);

    Task DeleteAsync(int id);
}