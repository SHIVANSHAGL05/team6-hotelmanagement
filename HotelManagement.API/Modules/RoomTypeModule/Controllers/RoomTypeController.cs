using HotelManagement.API.DTOs;
using HotelManagement.API.Modules.RoomTypeModule.DTOs;
using HotelManagement.API.Modules.RoomTypeModule.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Modules.RoomTypeModule.Controllers;

[ApiController]
[Route("api/roomtypes")]
public class RoomTypeController(IRoomTypeService roomTypeService) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll() =>
        Ok(new ApiResponse<object> { Success = true, Message = "Room types fetched.", Data = await roomTypeService.GetAllAsync() });

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await roomTypeService.GetByIdAsync(id);
        return item is null
            ? NotFound(new ApiResponse<object> { Success = false, Message = "Room type not found." })
            : Ok(new ApiResponse<object> { Success = true, Message = "Room type fetched.", Data = item });
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] RoomTypeCreateDto dto)
    {
        var created = await roomTypeService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.RoomTypeId },
            new ApiResponse<object> { Success = true, Message = "Room type created.", Data = created });
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] RoomTypeUpdateDto dto) =>
        await roomTypeService.UpdateAsync(id, dto)
            ? Ok(new ApiResponse<object> { Success = true, Message = "Room type updated." })
            : NotFound(new ApiResponse<object> { Success = false, Message = "Room type not found." });

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id) =>
        await roomTypeService.DeleteAsync(id)
            ? Ok(new ApiResponse<object> { Success = true, Message = "Room type deleted." })
            : NotFound(new ApiResponse<object> { Success = false, Message = "Room type not found." });

    [HttpGet("{id:int}/rooms")]
    [Authorize]
    public async Task<IActionResult> GetRooms(int id) =>
        Ok(new ApiResponse<object> { Success = true, Message = "Rooms fetched.", Data = await roomTypeService.GetRoomsByTypeIdAsync(id) });

    [HttpGet("by-capacity/{capacity:int}")]
    [Authorize]
    public async Task<IActionResult> GetByCapacity(int capacity) =>
        Ok(new ApiResponse<object> { Success = true, Message = "Room types fetched by capacity.", Data = await roomTypeService.GetByCapacityAsync(capacity) });

    [HttpGet("by-price")]
    [Authorize]
    public async Task<IActionResult> GetByPrice([FromQuery] decimal min, [FromQuery] decimal max) =>
        Ok(new ApiResponse<object> { Success = true, Message = "Room types fetched by price.", Data = await roomTypeService.GetByPriceRangeAsync(min, max) });

    [HttpPut("{id:int}/price")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdatePrice(int id, [FromBody] RoomTypePriceUpdateDto dto) =>
        await roomTypeService.UpdatePriceAsync(id, dto)
            ? Ok(new ApiResponse<object> { Success = true, Message = "Price updated." })
            : NotFound(new ApiResponse<object> { Success = false, Message = "Room type not found." });

    [HttpGet("available")]
    [Authorize]
    public async Task<IActionResult> GetAvailable() =>
        Ok(new ApiResponse<object> { Success = true, Message = "Available room types fetched.", Data = await roomTypeService.GetAvailableRoomTypesAsync() });
}