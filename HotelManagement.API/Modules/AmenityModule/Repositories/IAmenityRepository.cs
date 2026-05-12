using HotelManagement.Common.Models;

namespace HotelManagement.API.Modules.AmenityModule.Repositories;

public interface IAmenityRepository
{
    Task<List<Amenity>> GetAmenitiesByRoomIdAsync(int roomId);
    Task<int> CountByHotelIdAsync(int hotelId);
}