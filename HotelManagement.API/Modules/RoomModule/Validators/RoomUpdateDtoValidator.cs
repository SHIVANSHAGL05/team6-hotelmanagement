using HotelManagement.API.Modules.RoomModule.DTOs;

using FluentValidation;

namespace HotelManagement.API.Modules.RoomModule.Validators;

public static class RoomUpdateDtoValidator
{
    public static void Validate(RoomUpdateDto dto)
    {
        if (dto.RoomNumber <= 0)
            throw new ValidationException("Room number must be greater than 0.");

        if (dto.RoomNumber > 9999)
            throw new ValidationException("Room number cannot exceed 9999.");

        if (dto.RoomTypeId <= 0)
            throw new ValidationException("A valid room type ID is required.");
    }
}
