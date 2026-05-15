using FluentValidation;
using HotelManagement.API.Modules.ReviewModule.Controllers;
using HotelManagement.API.Modules.ReviewModule.DTOs;
using HotelManagement.API.Modules.ReviewModule.Services;
using HotelManagement.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HotelManagement.Tests.Controllers;

public class ReviewControllerTests
{
    private readonly Mock<IReviewService> _reviewServiceMock = new();
    private readonly ReviewController _controller;

    public ReviewControllerTests()
    {
        _controller = new ReviewController(_reviewServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithReviews()
    {
        var reviews = new List<ReviewDto>
        {
            new() { ReviewId = 1, ReservationId = 10, Rating = 5, Comment = "Excellent" }
        };
        _reviewServiceMock
            .Setup(service => service.GetAllAsync())
            .ReturnsAsync(reviews);

        var result = await _controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(ok.Value);
        Assert.True(response.Success);
        Assert.Same(reviews, response.Data);
        _reviewServiceMock.Verify(service => service.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetById_WhenReviewExists_ReturnsOk()
    {
        var review = new ReviewDto { ReviewId = 2, ReservationId = 11, Rating = 4 };
        _reviewServiceMock
            .Setup(service => service.GetByIdAsync(2))
            .ReturnsAsync(review);

        var result = await _controller.GetById(2);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(ok.Value);
        Assert.True(response.Success);
        Assert.Same(review, response.Data);
        _reviewServiceMock.Verify(service => service.GetByIdAsync(2), Times.Once);
    }

    [Fact]
    public async Task Create_WithValidReview_ReturnsCreatedAtAction()
    {
        var dto = new ReviewCreateDto { ReservationId = 12, Rating = 5 };
        var createdReview = new ReviewDto { ReviewId = 3, ReservationId = 12, Rating = 5 };
        _reviewServiceMock
            .Setup(service => service.CreateAsync(dto))
            .ReturnsAsync(createdReview);

        var result = await _controller.Create(dto);

        var created = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(ReviewController.GetById), created.ActionName);
        Assert.Equal(createdReview.ReviewId, created.RouteValues?["id"]);
        var response = Assert.IsType<ApiResponse<object>>(created.Value);
        Assert.True(response.Success);
        Assert.Same(createdReview, response.Data);
        _reviewServiceMock.Verify(service => service.CreateAsync(dto), Times.Once);
    }

    [Fact]
    public async Task Update_WhenReviewExists_ReturnsOk()
    {
        var dto = new ReviewUpdateDto { Rating = 4, Comment = "Good" };
        _reviewServiceMock
            .Setup(service => service.UpdateAsync(4, dto))
            .ReturnsAsync(true);

        var result = await _controller.Update(4, dto);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(ok.Value);
        Assert.True(response.Success);
        Assert.Equal("Review updated.", response.Message);
        _reviewServiceMock.Verify(service => service.UpdateAsync(4, dto), Times.Once);
    }

    [Fact]
    public async Task GetById_WhenReviewDoesNotExist_ReturnsNotFound()
    {
        _reviewServiceMock
            .Setup(service => service.GetByIdAsync(99))
            .ReturnsAsync((ReviewDto?)null);

        var result = await _controller.GetById(99);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(notFound.Value);
        Assert.False(response.Success);
        Assert.Equal("Review not found.", response.Message);
        _reviewServiceMock.Verify(service => service.GetByIdAsync(99), Times.Once);
    }

    [Fact]
    public async Task Update_WhenReviewDoesNotExist_ReturnsNotFound()
    {
        var dto = new ReviewUpdateDto { Rating = 4 };
        _reviewServiceMock
            .Setup(service => service.UpdateAsync(99, dto))
            .ReturnsAsync(false);

        var result = await _controller.Update(99, dto);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(notFound.Value);
        Assert.False(response.Success);
        Assert.Equal("Review not found.", response.Message);
        _reviewServiceMock.Verify(service => service.UpdateAsync(99, dto), Times.Once);
    }

    [Fact]
    public async Task Delete_WhenReviewDoesNotExist_ReturnsNotFound()
    {
        _reviewServiceMock
            .Setup(service => service.DeleteAsync(99))
            .ReturnsAsync(false);

        var result = await _controller.Delete(99);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(notFound.Value);
        Assert.False(response.Success);
        Assert.Equal("Review not found.", response.Message);
        _reviewServiceMock.Verify(service => service.DeleteAsync(99), Times.Once);
    }

    [Fact]
    public async Task Create_WhenReviewIsInvalid_ThrowsValidationException()
    {
        var dto = new ReviewCreateDto();
        _reviewServiceMock
            .Setup(service => service.CreateAsync(dto))
            .ThrowsAsync(new ValidationException("Rating must be between 1 and 5."));

        await Assert.ThrowsAsync<ValidationException>(() => _controller.Create(dto));
        _reviewServiceMock.Verify(service => service.CreateAsync(dto), Times.Once);
    }
}
