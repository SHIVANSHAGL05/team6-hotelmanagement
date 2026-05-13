using System.ComponentModel.DataAnnotations;

namespace HotelManagement.API.Modules.AmenityModule.DTOs;

public class AmenityDto
{
    public int AmenityId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class AmenityCreateDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }
}