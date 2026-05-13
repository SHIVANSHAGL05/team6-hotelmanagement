using HotelManagement.API.Modules.RoomTypeModule.DTOs;
using HotelManagement.Common.Models;
using HotelManagement.API.Modules.RoomTypeModule.Repositories;
using HotelManagement.API.Modules.RoomTypeModule.Validators;

namespace HotelManagement.API.Modules.RoomTypeModule.Services;

public class RoomTypeService(IRoomTypeRepository roomTypeRepository) : IRoomTypeService
{
    public async Task<List<RoomTypeDto>> GetAllAsync() =>
        (await roomTypeRepository.GetAllAsync()).Select(MapRoomType).ToList();

    public async Task<RoomTypeDto?> GetByIdAsync(int id) =>
        (await roomTypeRepository.GetByIdAsync(id)) is { } rt ? MapRoomType(rt) : null;

    public async Task<List<RoomDto>> GetRoomsByTypeIdAsync(int id) =>
        (await roomTypeRepository.GetRoomsByTypeIdAsync(id)).Select(MapRoom).ToList();

    public async Task<List<RoomTypeDto>> GetByCapacityAsync(int capacity) =>
        (await roomTypeRepository.GetByCapacityAsync(capacity)).Select(MapRoomType).ToList();

    public async Task<List<RoomTypeDto>> GetByPriceRangeAsync(decimal min, decimal max) =>
        (await roomTypeRepository.GetByPriceRangeAsync(min, max)).Select(MapRoomType).ToList();

    public async Task<List<RoomTypeDto>> GetAvailableRoomTypesAsync() =>
        (await roomTypeRepository.GetAvailableRoomTypesAsync()).Select(MapRoomType).ToList();

    public async Task<RoomTypeDto?> GetRoomTypeDetailsByIdAsync(int id) =>
        (await roomTypeRepository.GetByIdAsync(id)) is { } roomType ? MapRoomType(roomType) : null;

    public async Task<RoomTypeDto> CreateAsync(RoomTypeCreateDto dto)
    {
        RoomTypeCreateDtoValidator.Validate(dto);
        var roomType = new RoomType
        {
            TypeName = dto.TypeName,
            Description = dto.Description,
            MaxOccupancy = dto.MaxOccupancy,
            PricePerNight = dto.PricePerNight
        };
        var created = await roomTypeRepository.AddAsync(roomType);
        return MapRoomType(created);
    }

    public async Task<bool> UpdateAsync(int id, RoomTypeUpdateDto dto)
    {
        RoomTypeUpdateDtoValidator.Validate(dto);
        var existing = await roomTypeRepository.GetByIdAsync(id);
        if (existing is null) return false;
        existing.TypeName = dto.TypeName;
        existing.Description = dto.Description;
        existing.MaxOccupancy = dto.MaxOccupancy;
        existing.PricePerNight = dto.PricePerNight;
        await roomTypeRepository.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> UpdatePriceAsync(int id, RoomTypePriceUpdateDto dto)
    {
        var existing = await roomTypeRepository.GetByIdAsync(id);
        if (existing is null) return false;
        existing.PricePerNight = dto.PricePerNight;
        await roomTypeRepository.UpdateAsync(existing);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await roomTypeRepository.GetByIdAsync(id);
        if (existing is null) return false;
        await roomTypeRepository.DeleteAsync(existing);
        return true;
    }

    private static RoomTypeDto MapRoomType(RoomType rt) => new()
    {
        RoomTypeId = rt.RoomTypeId,
        TypeName = rt.TypeName ?? string.Empty,
        Description = rt.Description,
        MaxOccupancy = rt.MaxOccupancy,
        PricePerNight = rt.PricePerNight
    };

    private static RoomDto MapRoom(Room r) => new()
    {
        RoomId = r.RoomId,
        RoomNumber = r.RoomNumber,
        RoomTypeId = r.RoomTypeId,
        RoomTypeName = r.RoomType?.TypeName,
        IsAvailable = r.IsAvailable
    };
}
