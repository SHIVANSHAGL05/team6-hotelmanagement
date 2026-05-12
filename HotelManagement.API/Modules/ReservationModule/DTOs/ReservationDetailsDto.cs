using HotelManagement.API.DTOs;

namespace HotelManagement.API.Modules.ReservationModule.DTOs;

public class ReservationDetailsDto
{
    public int ReservationId { get; set; }
    public string GuestName { get; set; } = string.Empty;
    public string GuestEmail { get; set; } = string.Empty;
    public string GuestPhoneNumber { get; set; } = string.Empty;
    public DateOnly CheckInDate { get; set; }
    public DateOnly CheckOutDate { get; set; }
    public DateOnly BookingDate { get; set; }
    
    public int RoomNumber { get; set; }

    public int RoomTypeId { get; set; }
    public string RoomType { get; set; } = string.Empty;
    public decimal PricePerNight { get; set; }
    
    public List<AmenityDto> Amenities { get; set; } = [];

    public int HotelId { get; set; }
    public string HotelName { get; set; } = string.Empty;
    public string HotelLocation { get; set; } = string.Empty;

    public decimal TotalPrice { get; set; }
}