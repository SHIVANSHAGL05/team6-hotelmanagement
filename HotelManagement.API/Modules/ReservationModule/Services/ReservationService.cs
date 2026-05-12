using AutoMapper;
using HotelManagement.API.Modules.AmenityModule.Services;
using HotelManagement.API.Modules.PaymentModule.Services;
using HotelManagement.API.Modules.ReservationModule.DTOs;
using HotelManagement.API.Modules.ReservationModule.Exceptions;
using HotelManagement.API.Modules.ReservationModule.Repositories;
using HotelManagement.API.Modules.RoomModule.Services;
using HotelManagement.API.Modules.RoomTypeModule.Services;
using HotelManagement.Common.Models;

namespace HotelManagement.API.Modules.ReservationModule.Services;

public class ReservationService(
    IReservationRepository reservationRepository, 
    IRoomService roomService, 
    IRoomTypeService roomTypeService, 
    IAmenityService amenityService, 
    IPaymentService paymentService,
    IMapper mapper) : IReservationService
{
    private readonly IReservationRepository _reservationRepository = reservationRepository;
    private readonly IRoomService _roomService = roomService;
    private readonly IRoomTypeService _roomTypeService = roomTypeService;
    private readonly IAmenityService _amenityService = amenityService;
    private readonly IPaymentService _paymentService = paymentService;
    private readonly IMapper _mapper = mapper;
    
    public async Task<ReservationDetailsDto> GetReservationDetailsAsync(int reservationId)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
        if (reservation == null)
            throw new ReservationNotFoundException(reservationId);

        return await ToReservationDetailsDto(reservation);
    }

    public async Task<ReservationDetailsDto> CreateReservationAsync(CreateReservationDto reservationDto)
    {
        var reservation = _mapper.Map<Reservation>(reservationDto);
        await _reservationRepository.CreateReservationAsync(reservation);
        return await ToReservationDetailsDto(reservation);
    }

    public async Task<bool> HasReservationDateConflictAsync(int roomId, DateOnly checkIn, DateOnly checkOut)
        => await _reservationRepository.HasReservationDateConflictAsync(roomId, checkIn, checkOut);

    private async Task<ReservationDetailsDto> ToReservationDetailsDto(Reservation reservation)
    {
        var roomId =  reservation.RoomId ?? 
                      throw new ReservationNotFoundException(reservation.ReservationId);
        var room = await _roomService.GetRoomDetailsByIdAsync(roomId);
        var roomType = await _roomTypeService.GetRoomTypeDetailsByIdAsync(room.RoomTypeId);
        var amenities = await _amenityService.GetAmenitiesByRoomIdAsync(roomId);
        var paymentDetails = await _paymentService
            .GetPaymentDetailsByReservationIdAsync(reservation.ReservationId);

        var reservationDetails = new ReservationDetailsDto
        {
            ReservationId = reservation.ReservationId,
            GuestName = reservation.GuestName ?? "",
            GuestEmail = reservation.GuestEmail ?? "",
            GuestPhoneNumber = reservation.GuestPhone ?? "",
            CheckInDate = reservation.CheckInDate ?? new DateOnly(),
            CheckOutDate = reservation.CheckOutDate ?? new DateOnly(),
            BookingDate = paymentDetails.PaymentDate,
            RoomNumber = room.RoomNumber,
            RoomTypeId = room.RoomTypeId,
            RoomType = roomType.TypeName,
            Amenities = amenities,
            PricePerNight = roomType.PricePerNight,
            HotelId = 0,
            HotelName = string.Empty,
            HotelLocation = string.Empty,
            TotalPrice = paymentDetails.Amount
        };
        
        return reservationDetails;
    }
}
