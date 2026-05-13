using HotelManagement.API.Modules.ReviewModule.DTOs;

using FluentValidation;

namespace HotelManagement.API.Modules.ReviewModule.Validators;

public static class ReviewUpdateDtoValidator
{
    public static void Validate(ReviewUpdateDto dto)
    {
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
