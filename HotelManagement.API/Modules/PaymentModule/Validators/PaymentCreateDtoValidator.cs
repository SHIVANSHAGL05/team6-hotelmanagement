using FluentValidation;
using HotelManagement.API.DTOs;

namespace HotelManagement.API.Validators
{
    public class PaymentCreateDtoValidator : AbstractValidator<PaymentCreateDto>
    {
        public PaymentCreateDtoValidator()
        {
            RuleFor(x => x.ReservationId)
                .GreaterThan(0)
                .WithMessage("ReservationId must be greater than 0.");

            RuleFor(x => x.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than 0.");

            RuleFor(x => x.PaymentStatus)
                .NotEmpty()
                .WithMessage("Payment status is required.")
                .Must(status => new[] { "Pending", "Paid", "Failed", "Refunded" }
                .Contains(status))
                .WithMessage("Payment status must be Pending, Paid, Failed, or Refunded.");
        }
    }
}