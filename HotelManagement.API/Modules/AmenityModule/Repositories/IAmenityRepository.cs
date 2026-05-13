using HotelManagement.Common.Models;

namespace HotelManagement.API.Modules.AmenityModule.Repositories;

public interface IAmenityRepository
{
    Task<List<Amenity>> GetAllAsync();
    Task<Amenity?> GetByIdAsync(int id);
    Task<List<Amenity>> SearchByNameAsync(string name);
    Task<List<Hotel>> GetHotelsByAmenityIdAsync(int amenityId);
    Task<List<Room>> GetRoomsByAmenityIdAsync(int amenityId);
    Task<List<Amenity>> GetHotelOnlyAmenitiesAsync();
    Task<List<Amenity>> GetRoomOnlyAmenitiesAsync();
    Task<int> CountByHotelIdAsync(int hotelId);
    Task<Amenity> AddAsync(Amenity amenity);
    Task UpdateAsync(Amenity amenity);
    Task DeleteAsync(Amenity amenity);
}
