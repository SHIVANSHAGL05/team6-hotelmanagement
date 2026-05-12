namespace HotelManagement.API.DTOs
{
    public class PaymentUpdateDto
    {
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } = string.Empty;
    }
}