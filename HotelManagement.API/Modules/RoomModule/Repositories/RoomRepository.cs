using HotelManagement.Common.Data;
using HotelManagement.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Modules.RoomModule.Repositories;

public class RoomRepository(HotelDbContext context) : IRoomRepository
{
    public Task<List<Room>> GetAllAsync() =>
        context.Rooms.AsNoTracking().Include(r => r.RoomType).ToListAsync();

    public Task<Room?> GetByIdAsync(int id) =>
        context.Rooms.AsNoTracking().Include(r => r.RoomType)
            .FirstOrDefaultAsync(x => x.RoomId == id);

    public Task<List<Room>> GetAvailableRoomsAsync() =>
        context.Rooms.AsNoTracking().Include(r => r.RoomType)
            .Where(r => r.IsAvailable == true)
            .ToListAsync();

    public Task<List<Room>> GetByRoomTypeAsync(int roomTypeId) =>
        context.Rooms.AsNoTracking().Include(r => r.RoomType)
            .Where(r => r.RoomTypeId == roomTypeId)
            .ToListAsync();

    public Task<List<Amenity>> GetAmenitiesByRoomIdAsync(int roomId) =>
        context.Rooms.AsNoTracking()
            .Where(r => r.RoomId == roomId)
            .SelectMany(r => r.Amenities)
            .ToListAsync();

    public Task<List<Reservation>> GetReservationsByRoomIdAsync(int roomId) =>
        context.Reservations.AsNoTracking()
            .Where(res => res.RoomId == roomId)
            .ToListAsync();

    public async Task<Room> AddAsync(Room room)
    {
        context.Rooms.Add(room);
        await context.SaveChangesAsync();
        return room;
    }

    public async Task UpdateAsync(Room room)
    {
        context.Rooms.Update(room);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Room room)
    {
        context.Rooms.Remove(room);
        await context.SaveChangesAsync();
    }
}
