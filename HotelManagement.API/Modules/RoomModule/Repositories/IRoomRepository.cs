using HotelManagement.Common.Models;

namespace HotelManagement.API.Repositories;

public interface IRoomRepository
{
    Task<List<Room>> GetAllAsync();
    Task<Room?> GetByIdAsync(int id);
    Task<List<Room>> GetAvailableRoomsAsync();
    Task<List<Room>> GetByRoomTypeAsync(int roomTypeId);
    Task<List<Amenity>> GetAmenitiesByRoomIdAsync(int roomId);
    Task<List<Reservation>> GetReservationsByRoomIdAsync(int roomId);
    Task<Room> AddAsync(Room room);
    Task UpdateAsync(Room room);
    Task DeleteAsync(Room room);
}
