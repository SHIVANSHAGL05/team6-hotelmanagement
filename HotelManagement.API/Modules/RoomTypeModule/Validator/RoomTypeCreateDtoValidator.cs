using HotelManagement.API.DTOs;
using HotelManagement.API.Exceptions;

using FluentValidation;

namespace HotelManagement.API.Validators;

public static class RoomTypeCreateDtoValidator
{
    public static void Validate(RoomTypeCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.TypeName))
            throw new ValidationException("Room type name is required.");

        if (dto.TypeName.Length > 255)
            throw new ValidationException("Room type name cannot exceed 255 characters.");

        if (dto.Description?.Length > 2000)
            throw new ValidationException("Description cannot exceed 2000 characters.");

        if (dto.MaxOccupancy.HasValue && dto.MaxOccupancy < 1)
            throw new ValidationException("Max occupancy must be at least 1.");

        if (dto.PricePerNight.HasValue && dto.PricePerNight <= 0)
            throw new ValidationException("Price per night must be greater than 0.");
    }
}
