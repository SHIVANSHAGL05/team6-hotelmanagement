using HotelManagement.Common.Models;

namespace HotelManagement.API.Repositories;

public interface IRoomTypeRepository
{
    Task<List<RoomType>> GetAllAsync();
    Task<RoomType?> GetByIdAsync(int id);
    Task<List<RoomType>> GetByCapacityAsync(int capacity);
    Task<List<RoomType>> GetByPriceRangeAsync(decimal min, decimal max);
    Task<List<RoomType>> GetAvailableRoomTypesAsync();
    Task<List<Room>> GetRoomsByTypeIdAsync(int roomTypeId);
    Task<RoomType> AddAsync(RoomType roomType);
    Task UpdateAsync(RoomType roomType);
    Task DeleteAsync(RoomType roomType);
}
