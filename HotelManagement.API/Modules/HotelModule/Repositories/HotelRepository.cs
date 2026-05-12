using HotelManagement.Common.Data;
using HotelManagement.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Modules.HotelModule.Repositories;

public class HotelRepository : IHotelRepository
{
    private readonly HotelDbContext _context;

    public HotelRepository(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Hotel>> GetAllAsync()
        => await _context.Hotels.ToListAsync();

    public async Task<Hotel?> GetByIdAsync(int id)
        => await _context.Hotels.FindAsync(id);

    public async Task<Hotel> CreateAsync(Hotel hotel)
    {
        _context.Hotels.Add(hotel);
        await _context.SaveChangesAsync();
        return hotel;
    }

    public async Task<Hotel> UpdateAsync(Hotel hotel)
    {
        _context.Hotels.Update(hotel);
        await _context.SaveChangesAsync();
        return hotel;
    }

    public async Task DeleteAsync(Hotel hotel)
    {
        _context.Hotels.Remove(hotel);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Hotel>> SearchByLocationAsync(string location)
        => await _context.Hotels
            .Where(h => h.Location != null && h.Location.ToLower().Contains(location.ToLower()))
            .ToListAsync();

    public async Task<IEnumerable<Room>> GetRoomsByHotelIdAsync(int hotelId)
    {
        return await _context.Rooms
            .Include(r => r.RoomType)
            .ToListAsync();
    }

    public async Task<IEnumerable<Room>> GetAvailableRoomsByHotelIdAsync(int hotelId)
        => await _context.Rooms
            .Include(r => r.RoomType)
            .Where(r => r.IsAvailable == true)
            .ToListAsync();

    public async Task<IEnumerable<Reservation>> GetReservationsByHotelIdAsync(int hotelId)
        => await _context.Reservations
            .Include(r => r.Room)
            .ThenInclude(room => room != null ? room.RoomType : null)
            .ToListAsync();
}
