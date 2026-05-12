namespace HotelManagement.API.Modules.HotelModule.DTOs;

public class HotelCreateDto
{
    public string? Name { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
}

public class HotelUpdateDto
{
    public string? Name { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
}

public class HotelSearchDto
{
    public string? Location { get; set; }
}

public class HotelResponseDto
{
    public int HotelId { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
}

public class HotelSummaryDto
{
    public int HotelId { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }
    public int TotalRooms { get; set; }
    public int AvailableRooms { get; set; }
    public int TotalReservations { get; set; }
    public int TotalAmenities { get; set; }
}

public class RoomBriefDto
{
    public int RoomId { get; set; }
    public int? RoomNumber { get; set; }
    public bool? IsAvailable { get; set; }
    public string? RoomTypeName { get; set; }
    public decimal? PricePerNight { get; set; }
}

public class ReservationBriefDto
{
    public int ReservationId { get; set; }
    public string? GuestName { get; set; }
    public string? GuestEmail { get; set; }
    public DateOnly? CheckInDate { get; set; }
    public DateOnly? CheckOutDate { get; set; }
    public int? RoomNumber { get; set; }
}
