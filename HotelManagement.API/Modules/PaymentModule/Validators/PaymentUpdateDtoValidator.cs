using FluentValidation;
using HotelManagement.API.Modules.PaymentModule.DTOs;

namespace HotelManagement.API.Modules.PaymentModule.Validators;

public class PaymentUpdateDtoValidator : AbstractValidator<PaymentUpdateDto>
{
    public PaymentUpdateDtoValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than 0.");

        RuleFor(x => x.PaymentStatus)
            .NotEmpty()
            .WithMessage("Payment status is required.")
            .Must(status => new[] { "Pending", "Paid", "Success", "Failed", "Refunded" }
            .Contains(status))
            .WithMessage("Payment status must be Pending, Paid, Success, Failed, or Refunded.");
    }
}