using HotelManagement.API.Modules.HotelModule.DTOs;

namespace HotelManagement.API.Modules.HotelModule.Services;

public interface IHotelService
{
    Task<IEnumerable<HotelResponseDto>> GetAllAsync();
    Task<HotelResponseDto> GetByIdAsync(int id);
    Task<HotelResponseDto> CreateAsync(HotelCreateDto dto);
    Task<HotelResponseDto> UpdateAsync(int id, HotelUpdateDto dto);
    Task DeleteAsync(int id);
    Task<IEnumerable<HotelResponseDto>> SearchByLocationAsync(string location);
    Task<IEnumerable<RoomBriefDto>> GetRoomsAsync(int hotelId);
    Task<IEnumerable<RoomBriefDto>> GetAvailableRoomsAsync(int hotelId);
    Task<IEnumerable<ReservationBriefDto>> GetReservationsAsync(int hotelId);
    Task<HotelSummaryDto> GetSummaryAsync(int hotelId);
}
