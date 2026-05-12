using FluentValidation;
using HotelManagement.API.Modules.HotelModule.DTOs;

namespace HotelManagement.API.Modules.HotelModule.Validators;

public class HotelUpdateDtoValidator : AbstractValidator<HotelUpdateDto>
{
    public HotelUpdateDtoValidator()
    {
        RuleFor(hotel => hotel.Name)
            .MinimumLength(3)
            .WithMessage("Hotel name should be at least 3 characters long.")
            .MaximumLength(100)
            .WithMessage("Hotel name should not exceed 100 characters.")
            .When(hotel => !string.IsNullOrEmpty(hotel.Name));

        RuleFor(hotel => hotel.Location)
            .MinimumLength(3)
            .WithMessage("Location should be at least 3 characters long.")
            .MaximumLength(100)
            .WithMessage("Location should not exceed 100 characters.")
            .When(hotel => !string.IsNullOrEmpty(hotel.Location));

        RuleFor(hotel => hotel.Description)
            .MaximumLength(500)
            .WithMessage("Description should not exceed 500 characters.")
            .When(hotel => !string.IsNullOrEmpty(hotel.Description));
    }
}
