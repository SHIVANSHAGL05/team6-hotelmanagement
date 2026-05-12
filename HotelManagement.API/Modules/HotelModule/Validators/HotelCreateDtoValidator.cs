using FluentValidation;
using HotelManagement.API.Modules.HotelModule.DTOs;

namespace HotelManagement.API.Modules.HotelModule.Validators;

public class HotelDtoValidator : AbstractValidator<HotelDto>
{
    public HotelDtoValidator()
    {
        RuleFor(hotel => hotel.Name)
            .NotEmpty()
            .WithMessage("Hotel name is required.")
            .MinimumLength(3)
            .WithMessage("Hotel name should be at least 3 characters long.")
            .MaximumLength(100)
            .WithMessage("Hotel name should not exceed 100 characters.");

        RuleFor(hotel => hotel.Location)
            .NotEmpty()
            .WithMessage("Location is required.")
            .MinimumLength(3)
            .WithMessage("Location should be at least 3 characters long.")
            .MaximumLength(100)
            .WithMessage("Location should not exceed 100 characters.");

        RuleFor(hotel => hotel.Description)
            .MaximumLength(500)
            .WithMessage("Description should not exceed 500 characters.");
    }
}
