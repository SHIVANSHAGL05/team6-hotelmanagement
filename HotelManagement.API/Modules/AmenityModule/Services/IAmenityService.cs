using HotelManagement.API.Modules.AmenityModule.DTOs;

namespace HotelManagement.API.Modules.AmenityModule.Services;

public interface IAmenityService
{
    Task<List<AmenityDto>> GetAllAsync();
    Task<AmenityDto?> GetByIdAsync(int id);
    Task<List<AmenityDto>> SearchByNameAsync(string name);
    Task<List<object>> GetHotelsByAmenityAsync(int id);
    Task<List<object>> GetRoomsByAmenityAsync(int id);
    Task<List<AmenityDto>> GetHotelOnlyAsync();
    Task<List<AmenityDto>> GetRoomOnlyAsync();
    Task<AmenityDto> CreateAsync(AmenityCreateDto dto);
    Task<bool> UpdateAsync(int id, AmenityCreateDto dto);
    Task<bool> DeleteAsync(int id);
}
