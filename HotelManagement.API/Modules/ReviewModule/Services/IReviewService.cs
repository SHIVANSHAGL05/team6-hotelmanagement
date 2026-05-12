using HotelManagement.API.DTOs;

namespace HotelManagement.API.Services;

public interface IReviewService
{
    Task<List<ReviewDto>> GetAllAsync();
    Task<ReviewDto?> GetByIdAsync(int id);
    Task<List<ReviewDto>> GetByReservationIdAsync(int reservationId);
    Task<List<ReviewDto>> GetByRatingAsync(int rating);
    Task<List<ReviewDto>> GetLatestAsync();
    Task<double> GetAverageRatingAsync();
    Task<List<ReviewDto>> GetTopRatedAsync();
    Task<ReviewDto> CreateAsync(ReviewCreateDto dto);
    Task<bool> UpdateAsync(int id, ReviewUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
