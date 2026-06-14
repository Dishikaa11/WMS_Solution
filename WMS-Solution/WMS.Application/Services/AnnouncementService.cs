using WMS.Application.DTOs;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Domain.Interfaces;

namespace WMS.Application.Services;

public class AnnouncementService : IAnnouncementService
{
    private readonly IAnnouncementRepository _announcementRepository;

    public AnnouncementService(
        IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }

    public async Task<List<AnnouncementDto>> GetAllAsync()
    {
        var announcements =
            await _announcementRepository.GetAllAsync();

        return announcements.Select(a => new AnnouncementDto
        {
            AnnouncementId = a.AnnouncementId,
            Title = a.Title,
            Message = a.Message,
            CreatedBy = a.CreatedBy,
            CreatedOn = a.CreatedOn,
            IsActive = a.IsActive
        }).ToList();
    }

    public async Task<AnnouncementDto?> GetByIdAsync(int id)
    {
        var announcement =
            await _announcementRepository.GetByIdAsync(id);

        if (announcement == null)
            return null;

        return new AnnouncementDto
        {
            AnnouncementId = announcement.AnnouncementId,
            Title = announcement.Title,
            Message = announcement.Message,
            CreatedBy = announcement.CreatedBy,
            CreatedOn = announcement.CreatedOn,
            IsActive = announcement.IsActive
        };
    }

    public async Task AddAsync(AnnouncementDto dto)
    {
        var announcement = new Announcement
        {
            Title = dto.Title,
            Message = dto.Message,
            CreatedBy = dto.CreatedBy,

            // Auto values
            CreatedOn = DateTime.Now,
            IsActive = true
        };

        await _announcementRepository.AddAsync(announcement);
        await _announcementRepository.SaveChangesAsync();
    }

    public async Task UpdateAsync(
        int id,
        AnnouncementDto dto)
    {
        var announcement =
            await _announcementRepository.GetByIdAsync(id);

        if (announcement == null)
            throw new Exception("Announcement not found");

        announcement.Title = dto.Title;
        announcement.Message = dto.Message;
        announcement.CreatedBy = dto.CreatedBy;
        announcement.IsActive = dto.IsActive;

        await _announcementRepository.UpdateAsync(announcement);
        await _announcementRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var announcement =
            await _announcementRepository.GetByIdAsync(id);

        if (announcement == null)
            throw new Exception("Announcement not found");

        await _announcementRepository.DeleteAsync(announcement);
        await _announcementRepository.SaveChangesAsync();
    }
}