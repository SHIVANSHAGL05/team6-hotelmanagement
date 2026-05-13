using HotelManagement.API.Modules.AmenityModule.DTOs;

using FluentValidation;

namespace HotelManagement.API.Modules.AmenityModule.Validators;

public static class AmenityCreateDtoValidator
{
    public static void Validate(AmenityCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new ValidationException("Amenity name is required.");
        }

        if (dto.Name.Length > 100)
        {
            throw new ValidationException("Amenity name cannot exceed 100 characters.");
        }

        if (dto.Description?.Length > 500)
        {
            throw new ValidationException("Amenity description cannot exceed 500 characters.");
        }
    }
}
