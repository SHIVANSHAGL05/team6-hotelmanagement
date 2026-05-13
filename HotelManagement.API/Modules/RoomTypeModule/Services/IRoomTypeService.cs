using HotelManagement.API.Modules.RoomTypeModule.DTOs;

namespace HotelManagement.API.Modules.RoomTypeModule.Services;

public interface IRoomTypeService
{
    Task<List<RoomTypeDto>> GetAllAsync();
    Task<RoomTypeDto?> GetByIdAsync(int id);
    Task<List<RoomDto>> GetRoomsByTypeIdAsync(int id);
    Task<List<RoomTypeDto>> GetByCapacityAsync(int capacity);
    Task<List<RoomTypeDto>> GetByPriceRangeAsync(decimal min, decimal max);
    Task<List<RoomTypeDto>> GetAvailableRoomTypesAsync();
    Task<RoomTypeDto?> GetRoomTypeDetailsByIdAsync(int id);
    Task<RoomTypeDto> CreateAsync(RoomTypeCreateDto dto);
    Task<bool> UpdateAsync(int id, RoomTypeUpdateDto dto);
    Task<bool> UpdatePriceAsync(int id, RoomTypePriceUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
