using HotelManagement.API.DTOs;
using HotelManagement.API.Exceptions;

using FluentValidation;

namespace HotelManagement.API.Validators;

public static class ReviewCreateDtoValidator
{
    public static void Validate(ReviewCreateDto dto)
    {
        if (dto.ReservationId <= 0)
        {
            throw new ValidationException("Reservation id must be greater than zero.");
        }

        if (dto.Rating is < 1 or > 5)
        {
            throw new ValidationException("Rating must be between 1 and 5.");
        }

        if (dto.Comment?.Length > 1000)
        {
            throw new ValidationException("Review comment cannot exceed 1000 characters.");
        }
    }
}
