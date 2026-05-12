using HotelManagement.Common.Data;
using HotelManagement.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.API.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly HotelDbContext _context;

        public PaymentRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments
                .OrderBy(p => p.ReservationId)
                .ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.PaymentId == id);
        }

        public async Task<Payment> AddAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment?> UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
                return false;

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ReservationExistsAsync(int reservationId)
        {
            return await _context.Reservations
                .AnyAsync(r => r.ReservationId == reservationId);
        }

        public async Task<IEnumerable<Payment>> GetByReservationAsync(int reservationId)
        {
            return await _context.Payments
                .Where(p => p.ReservationId == reservationId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Payment>> GetByStatusAsync(string status)
        {
            return await _context.Payments
                .Where(p => p.PaymentStatus == status)
                .ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}