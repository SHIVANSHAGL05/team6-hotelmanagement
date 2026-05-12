using FluentValidation;
using HotelManagement.API.Modules.HotelModule.DTOs;
using HotelManagement.API.Modules.HotelModule.Services;
using HotelManagement.API.Modules.HotelModule.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HotelManagement.API.Modules.HotelModule.Controllers

{
    [ApiController]
    [Route("api/hotels")]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _service;
        private readonly IValidator<HotelDto> _createValidator;
        private readonly IValidator<HotelDto> _updateValidator;

        public HotelController(
            IHotelService service,
            IValidator<HotelDto> createValidator,
            IValidator<HotelDto> updateValidator)
        {
            _service = service;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        // GET /api/hotels
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hotels = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<HotelResponseDto>>.Ok(hotels));
        }

        // GET /api/hotels/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var hotel = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<HotelResponseDto>.Ok(hotel));
        }

        // POST /api/hotels
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HotelDto dto)
        {
            var validation = await _createValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return UnprocessableEntity(ApiResponse<object>.Fail("Validation failed.",
                    validation.Errors.Select(e => e.ErrorMessage).ToList()));

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.HotelId },
                ApiResponse<HotelResponseDto>.Ok(created, "Hotel created successfully."));
        }

        // PUT /api/hotels/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] HotelDto dto)
        {
            var validation = await _updateValidator.ValidateAsync(dto);
            if (!validation.IsValid)
                return UnprocessableEntity(ApiResponse<object>.Fail("Validation failed.",
                    validation.Errors.Select(e => e.ErrorMessage).ToList()));

            var updated = await _service.UpdateAsync(id, dto);
            return Ok(ApiResponse<HotelResponseDto>.Ok(updated, "Hotel updated successfully."));
        }

        // DELETE /api/hotels/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(ApiResponse<object>.Ok(null!, "Hotel deleted successfully."));
        }

        // GET /api/hotels/search?location={location}
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string location)
        {
            if (string.IsNullOrWhiteSpace(location))
                return BadRequest(ApiResponse<object>.Fail("Location query parameter is required."));

            var hotels = await _service.SearchByLocationAsync(location);
            return Ok(ApiResponse<IEnumerable<HotelResponseDto>>.Ok(hotels));
        }

        // GET /api/hotels/{id}/rooms
        [HttpGet("{id:int}/rooms")]
        public async Task<IActionResult> GetRooms(int id)
        {
            var rooms = await _service.GetRoomsAsync(id);
            return Ok(ApiResponse<IEnumerable<RoomBriefDto>>.Ok(rooms));
        }

        // GET /api/hotels/{id}/available-rooms
        [HttpGet("{id:int}/available-rooms")]
        public async Task<IActionResult> GetAvailableRooms(int id)
        {
            var rooms = await _service.GetAvailableRoomsAsync(id);
            return Ok(ApiResponse<IEnumerable<RoomBriefDto>>.Ok(rooms));
        }

        // GET /api/hotels/{id}/reservations
        [HttpGet("{id:int}/reservations")]
        public async Task<IActionResult> GetReservations(int id)
        {
            var reservations = await _service.GetReservationsAsync(id);
            return Ok(ApiResponse<IEnumerable<ReservationBriefDto>>.Ok(reservations));
        }

        // GET /api/hotels/{id}/summary
        [HttpGet("{id:int}/summary")]
        public async Task<IActionResult> GetSummary(int id)
        {
            var summary = await _service.GetSummaryAsync(id);
            return Ok(ApiResponse<HotelSummaryDto>.Ok(summary));
        }
    }
}
