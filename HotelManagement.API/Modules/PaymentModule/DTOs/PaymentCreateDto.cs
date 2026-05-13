namespace HotelManagement.API.Modules.PaymentModule.DTOs;

public class PaymentCreateDto
{
    public int ReservationId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentStatus { get; set; } = "Pending";
}