using HotelManagement.API.DTOs;
using HotelManagement.API.Modules.PaymentModule.DTOs;
using HotelManagement.Common.Models;

namespace HotelManagement.API.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();
        Task<Payment?> GetPaymentByIdAsync(int id);
        Task<Payment?> CreatePaymentAsync(PaymentCreateDto dto);
        Task<Payment?> UpdatePaymentAsync(int id, PaymentUpdateDto dto);
        Task<bool> DeletePaymentAsync(int id);
        Task<IEnumerable<Payment>> GetPaymentsByReservationAsync(int reservationId);
        Task<IEnumerable<Payment>> GetSuccessfulPaymentsAsync();
        Task<IEnumerable<Payment>> GetFailedPaymentsAsync();
        Task<Payment?> UpdatePaymentStatusAsync(int id, PaymentStatusUpdateDto dto);
        Task<Payment?> RefundPaymentAsync(int id);
        Task<PaymentDetailsDto?> GetPaymentDetailsByReservationIdAsync(int reservationId);
    }
}