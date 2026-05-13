using HotelManagement.API.Modules.RoomModule.DTOs;
using HotelManagement.API.Modules.AmenityModule.DTOs;
using HotelManagement.Common.Models;
using HotelManagement.API.Modules.RoomModule.Repositories;
using HotelManagement.API.Modules.RoomModule.Validators;

namespace HotelManagement.API.Modules.RoomModule.Services;

public class RoomService(IRoomRepository roomRepository) : IRoomService
{
    public async Task<List<RoomDto>> GetAllAsync() =>
        (await roomRepository.GetAllAsync()).Select(MapRoom).ToList();

    public async Task<RoomDto?> GetByIdAsync(int id) =>
        (await roomRepository.GetByIdAsync(id)) is { } room ? MapRoom(room) : null;

    public async Task<List<RoomDto>> GetAvailableRoomsAsync() =>
        (await roomRepository.GetAvailableRoomsAsync()).Select(MapRoom).ToList();

    public async Task<List<RoomDto>> GetByRoomTypeAsync(int roomTypeId) =>
        (await roomRepository.GetByRoomTypeAsync(roomTypeId)).Select(MapRoom).ToList();

    public async Task<RoomDto?> GetRoomDetailsByIdAsync(int id) =>
        (await roomRepository.GetByIdAsync(id)) is { } room ? MapRoom(room) : null;

    public async Task<List<AmenityDto>> GetAmenitiesByRoomIdAsync(int id) =>
        (await roomRepository.GetAmenitiesByRoomIdAsync(id))
            .Select(a => new AmenityDto
            {
                AmenityId = a.AmenityId,
                Name = a.Name ?? string.Empty,
                Description = a.Description
            })
            .ToList();

    public async Task<List<object>> GetReservationsByRoomIdAsync(int id) =>
        (await roomRepository.GetReservationsByRoomIdAsync(id))
            .Select(r => (object)new
            {
                r.ReservationId,
                r.GuestName,
                r.GuestEmail,
                r.CheckInDate,
                r.CheckOutDate
            }).ToList();

    public async Task<RoomDto> CreateAsync(RoomCreateDto dto)
    {
        RoomCreateDtoValidator.Validate(dto);
        var room = new Room
        {
            RoomNumber = dto.RoomNumber,
            RoomTypeId = dto.RoomTypeId,
            IsAvailable = dto.IsAvailable
        };
        var created = await roomRepository.AddAsync(room);
        return MapRoom(created);
    }

    public async Task<bool> UpdateAsync(int id, RoomUpdateDto dto)
    {
        RoomUpdateDtoValidator.Validate(dto);
        var existing = await roomRepository.GetByIdAsync(id);
        if (existing is null) return false;
        existing.RoomNumber = dto.RoomNumber;
        existing.RoomTypeId = dto.RoomTypeId;
        existing.IsAvailable = dto.IsAvailable;
        await roomRepository.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> UpdateAvailabilityAsync(int id, RoomAvailabilityUpdateDto dto)
    {
        var existing = await roomRepository.GetByIdAsync(id);
        if (existing is null) return false;
        existing.IsAvailable = dto.IsAvailable;
        await roomRepository.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await roomRepository.GetByIdAsync(id);
        if (existing is null) return false;
        await roomRepository.DeleteAsync(existing);
        return true;
    }

    private static RoomDto MapRoom(Room r) => new()
    {
        RoomId = r.RoomId,
        RoomNumber = r.RoomNumber,
        RoomTypeId = r.RoomTypeId,
        RoomTypeName = r.RoomType?.TypeName,
        IsAvailable = r.IsAvailable
    };
}
