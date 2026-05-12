using HotelManagement.API.DTOs;
using HotelManagement.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _service.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _service.GetPaymentByIdAsync(id);

            if (payment == null)
                return NotFound("Payment not found.");

            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(PaymentCreateDto dto)
        {
            var payment = await _service.CreatePaymentAsync(dto);

            if (payment == null)
                return BadRequest("Reservation does not exist.");

            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentId }, payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, PaymentUpdateDto dto)
        {
            var payment = await _service.UpdatePaymentAsync(id, dto);

            if (payment == null)
                return NotFound("Payment not found.");

            return Ok(payment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _service.DeletePaymentAsync(id);

            if (!result)
                return NotFound("Payment not found.");

            return Ok("Payment deleted successfully.");
        }

        [HttpGet("by-reservation/{reservationId}")]
        public async Task<IActionResult> GetPaymentsByReservation(int reservationId)
        {
            var payments = await _service.GetPaymentsByReservationAsync(reservationId);
            return Ok(payments);
        }

        [HttpGet("successful")]
        public async Task<IActionResult> GetSuccessfulPayments()
        {
            var payments = await _service.GetSuccessfulPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("failed")]
        public async Task<IActionResult> GetFailedPayments()
        {
            var payments = await _service.GetFailedPaymentsAsync();
            return Ok(payments);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdatePaymentStatus(int id, PaymentStatusUpdateDto dto)
        {
            var payment = await _service.UpdatePaymentStatusAsync(id, dto);

            if (payment == null)
                return NotFound("Payment not found.");

            return Ok(payment);
        }


        [HttpPost("{id}/refund")]
        public async Task<IActionResult> RefundPayment(int id)
        {
            var payment = await _service.RefundPaymentAsync(id);

            if (payment == null)
                return BadRequest("Payment not found or only successful payments can be refunded.");

            return Ok(payment);
        }
    }
}