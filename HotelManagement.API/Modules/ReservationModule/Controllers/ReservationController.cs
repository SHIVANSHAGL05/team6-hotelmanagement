using System.Security.Claims;
using HotelManagement.API.DTOs;
using HotelManagement.API.Modules.ReservationModule.DTOs;
using HotelManagement.API.Modules.ReservationModule.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Modules.ReservationModule.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReservationController(IReservationService reservationService) : ControllerBase
{
    private readonly IReservationService _reservationService = reservationService;

    [HttpGet("{reservationId:int}")]
    public async Task<IActionResult> GetReservationById(int reservationId)
    {
        var reservation = await _reservationService.GetReservationDetailsAsync(reservationId);
        var response = new ApiResponse<ReservationDetailsDto>
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Successfully retrieved reservation.",
            Data = reservation,
            Errors = null,
            Timestamp = DateTime.UtcNow
        };
        return Ok(response);
    }

    [HttpGet("my-reservations")]
    public async Task<IActionResult> GetMyReservations()
    {
        var user = User.FindFirst(ClaimTypes.Email) ?? throw new NullReferenceException("Email");
        var userEmail = user.Value;
        var reservations = await _reservationService.GetReservationsByUserEmailAsync(userEmail!);
        var response = new ApiResponse<List<ReservationDetailsDto>>
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Successfully retrieved reservations.",
            Data = reservations,
            Errors = null,
            Timestamp = DateTime.UtcNow
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto createReservationDto)
    {
        var reservation = await _reservationService.CreateReservationAsync(createReservationDto);
        var response = new ApiResponse<string?>
        {
            Success = true,
            StatusCode = StatusCodes.Status201Created,
            Message = "Successfully created reservation.",
            Data = null,
            Errors = null,
            Timestamp = DateTime.UtcNow
        };
        return CreatedAtAction(nameof(CreateReservation), new { reservation.ReservationId }, response); 
    }
}
