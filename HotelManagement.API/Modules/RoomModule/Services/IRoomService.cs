using HotelManagement.API.Modules.AmenityModule.DTOs;
using HotelManagement.API.Modules.RoomModule.DTOs;

namespace HotelManagement.API.Modules.RoomModule.Services;

public interface IRoomService
{
    Task<List<RoomDto>> GetAllAsync();
    Task<RoomDto?> GetByIdAsync(int id);
    Task<List<RoomDto>> GetAvailableRoomsAsync();
    Task<List<RoomDto>> GetByRoomTypeAsync(int roomTypeId);
    Task<RoomDto?> GetRoomDetailsByIdAsync(int id);
    Task<List<AmenityDto>> GetAmenitiesByRoomIdAsync(int id);
    Task<List<object>> GetReservationsByRoomIdAsync(int id);
    Task<RoomDto> CreateAsync(RoomCreateDto dto);
    Task<bool> UpdateAsync(int id, RoomUpdateDto dto);
    Task<bool> UpdateAvailabilityAsync(int id, RoomAvailabilityUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
