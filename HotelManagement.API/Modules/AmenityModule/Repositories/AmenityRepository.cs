using HotelManagement.Common.Data;
using HotelManagement.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Modules.AmenityModule.Repositories;

public class AmenityRepository(HotelDbContext context) : IAmenityRepository
{
    public Task<List<Amenity>> GetAllAsync() => context.Amenities.AsNoTracking().ToListAsync();

    public Task<Amenity?> GetByIdAsync(int id) => context.Amenities.AsNoTracking().FirstOrDefaultAsync(x => x.AmenityId == id);

    public Task<List<Amenity>> SearchByNameAsync(string name) =>
        context.Amenities.AsNoTracking().Where(x => x.Name != null && x.Name.Contains(name)).ToListAsync();

    public Task<List<Hotel>> GetHotelsByAmenityIdAsync(int amenityId) =>
        context.Amenities.AsNoTracking().Where(x => x.AmenityId == amenityId).SelectMany(x => x.Hotels).ToListAsync();

    public Task<List<Room>> GetRoomsByAmenityIdAsync(int amenityId) =>
        context.Amenities.AsNoTracking().Where(x => x.AmenityId == amenityId).SelectMany(x => x.Rooms).ToListAsync();

    public Task<List<Amenity>> GetHotelOnlyAmenitiesAsync() =>
        context.Amenities.AsNoTracking().Where(x => x.Hotels.Any() && !x.Rooms.Any()).ToListAsync();

    public Task<List<Amenity>> GetRoomOnlyAmenitiesAsync() =>
        context.Amenities.AsNoTracking().Where(x => x.Rooms.Any() && !x.Hotels.Any()).ToListAsync();

    public Task<int> CountByHotelIdAsync(int hotelId) =>
        context.Hotels.AsNoTracking()
            .Where(h => h.HotelId == hotelId)
            .SelectMany(h => h.Amenities)
            .CountAsync();

    public async Task<Amenity> AddAsync(Amenity amenity)
    {
        context.Amenities.Add(amenity);
        await context.SaveChangesAsync();
        return amenity;
    }

    public async Task UpdateAsync(Amenity amenity)
    {
        context.Amenities.Update(amenity);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Amenity amenity)
    {
        context.Amenities.Remove(amenity);
        await context.SaveChangesAsync();
    }
}
