using HotelManagement.Common.Data;
using HotelManagement.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Modules.AmenityModule.Repositories;

public class AmenityRepository(HotelDbContext dbContext) : IAmenityRepository
{
    private readonly HotelDbContext _dbContext = dbContext;

    public async Task<List<Amenity>> GetAmenitiesByRoomIdAsync(int roomId) =>
        await _dbContext.Amenities.Where(a => a.Rooms.Any(r => r.RoomId == roomId)).ToListAsync();

    public async Task<int> CountByHotelIdAsync(int hotelId) =>
        await _dbContext.Hotels
            .Where(hotel => hotel.HotelId == hotelId)
            .Select(hotel => hotel.Amenities.Count)
            .FirstOrDefaultAsync();
}