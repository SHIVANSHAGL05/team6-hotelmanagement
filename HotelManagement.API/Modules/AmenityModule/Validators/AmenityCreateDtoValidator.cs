using HotelManagement.API.DTOs;
using HotelManagement.API.Exceptions;

using FluentValidation;

namespace HotelManagement.API.Validators;

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
