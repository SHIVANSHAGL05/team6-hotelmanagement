using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Common.Models;

public class ApplicationUser : IdentityUser
{
    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}