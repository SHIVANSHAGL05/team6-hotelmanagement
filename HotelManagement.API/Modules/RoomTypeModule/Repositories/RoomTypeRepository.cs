using HotelManagement.Common.Data;
using HotelManagement.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Modules.RoomTypeModule.Repositories;

public class RoomTypeRepository(HotelDbContext context) : IRoomTypeRepository
{
    public Task<List<RoomType>> GetAllAsync() =>
        context.RoomTypes.AsNoTracking().ToListAsync();

    public Task<RoomType?> GetByIdAsync(int id) =>
        context.RoomTypes.AsNoTracking().FirstOrDefaultAsync(x => x.RoomTypeId == id);

    public Task<List<RoomType>> GetByCapacityAsync(int capacity) =>
        context.RoomTypes.AsNoTracking()
            .Where(x => x.MaxOccupancy >= capacity)
            .ToListAsync();

    public Task<List<RoomType>> GetByPriceRangeAsync(decimal min, decimal max) =>
        context.RoomTypes.AsNoTracking()
            .Where(x => x.PricePerNight >= min && x.PricePerNight <= max)
            .ToListAsync();

    public Task<List<RoomType>> GetAvailableRoomTypesAsync() =>
        context.RoomTypes.AsNoTracking()
            .Where(rt => rt.Rooms.Any(r => r.IsAvailable == true))
            .ToListAsync();

    public Task<List<Room>> GetRoomsByTypeIdAsync(int roomTypeId) =>
        context.Rooms.AsNoTracking()
            .Where(r => r.RoomTypeId == roomTypeId)
            .ToListAsync();

    public async Task<RoomType> AddAsync(RoomType roomType)
    {
        context.RoomTypes.Add(roomType);
        await context.SaveChangesAsync();
        return roomType;
    }

    public async Task UpdateAsync(RoomType roomType)
    {
        context.RoomTypes.Update(roomType);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(RoomType roomType)
    {
        context.RoomTypes.Remove(roomType);
        await context.SaveChangesAsync();
    }
}
