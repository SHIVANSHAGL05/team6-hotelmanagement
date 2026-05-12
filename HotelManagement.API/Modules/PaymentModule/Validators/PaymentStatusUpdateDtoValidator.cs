using FluentValidation;
using HotelManagement.API.DTOs;

namespace HotelManagement.API.Validators
{
    public class PaymentStatusUpdateDtoValidator : AbstractValidator<PaymentStatusUpdateDto>
    {
        public PaymentStatusUpdateDtoValidator()
        {
            RuleFor(x => x.PaymentStatus)
                .NotEmpty()
                .WithMessage("Payment status is required.")
                .Must(status => new[] { "Pending", "Paid", "Success", "Failed", "Refunded" }
                .Contains(status))
                .WithMessage("Payment status must be Pending, Paid, Success, Failed, or Refunded.");
        }
    }
}