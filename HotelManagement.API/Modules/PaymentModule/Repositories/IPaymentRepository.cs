using HotelManagement.Common.Models;

namespace HotelManagement.API.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment?> GetByIdAsync(int id);
        Task<Payment> AddAsync(Payment payment);
        Task<Payment?> UpdateAsync(Payment payment);
        Task<bool> DeleteAsync(int id);
        Task<bool> ReservationExistsAsync(int reservationId);
        Task<IEnumerable<Payment>> GetByReservationAsync(int reservationId);
        Task<IEnumerable<Payment>> GetByStatusAsync(string status);
        Task SaveAsync();
    }
}