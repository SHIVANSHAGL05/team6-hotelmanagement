using FluentValidation;
using HotelManagement.API.DTOs;
using HotelManagement.API.Modules.AmenityModule.Controllers;
using HotelManagement.API.Modules.AmenityModule.DTOs;
using HotelManagement.API.Modules.AmenityModule.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HotelManagement.Tests.Controllers;

public class AmenityControllerTests
{
    private readonly Mock<IAmenityService> _amenityServiceMock = new();
    private readonly AmenityController _controller;

    public AmenityControllerTests()
    {
        _controller = new AmenityController(_amenityServiceMock.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk_WithAmenities()
    {
        var amenities = new List<AmenityDto>
        {
            new() { AmenityId = 1, Name = "WiFi", Description = "High speed internet" }
        };
        _amenityServiceMock
            .Setup(service => service.GetAllAsync())
            .ReturnsAsync(amenities);

        var result = await _controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(ok.Value);
        Assert.True(response.Success);
        Assert.Same(amenities, response.Data);
        _amenityServiceMock.Verify(service => service.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task GetById_WhenAmenityExists_ReturnsOk()
    {
        var amenity = new AmenityDto { AmenityId = 2, Name = "Pool" };
        _amenityServiceMock
            .Setup(service => service.GetByIdAsync(2))
            .ReturnsAsync(amenity);

        var result = await _controller.GetById(2);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(ok.Value);
        Assert.True(response.Success);
        Assert.Same(amenity, response.Data);
        _amenityServiceMock.Verify(service => service.GetByIdAsync(2), Times.Once);
    }

    [Fact]
    public async Task Create_WithValidAmenity_ReturnsCreatedAtAction()
    {
        var dto = new AmenityCreateDto { Name = "Spa" };
        var createdAmenity = new AmenityDto { AmenityId = 3, Name = "Spa" };
        _amenityServiceMock
            .Setup(service => service.CreateAsync(dto))
            .ReturnsAsync(createdAmenity);

        var result = await _controller.Create(dto);

        var created = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(nameof(AmenityController.GetById), created.ActionName);
        Assert.Equal(createdAmenity.AmenityId, created.RouteValues?["id"]);
        var response = Assert.IsType<ApiResponse<object>>(created.Value);
        Assert.True(response.Success);
        Assert.Same(createdAmenity, response.Data);
        _amenityServiceMock.Verify(service => service.CreateAsync(dto), Times.Once);
    }

    [Fact]
    public async Task Update_WhenAmenityExists_ReturnsOk()
    {
        var dto = new AmenityCreateDto { Name = "Parking" };
        _amenityServiceMock
            .Setup(service => service.UpdateAsync(4, dto))
            .ReturnsAsync(true);

        var result = await _controller.Update(4, dto);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(ok.Value);
        Assert.True(response.Success);
        Assert.Equal("Amenity updated.", response.Message);
        _amenityServiceMock.Verify(service => service.UpdateAsync(4, dto), Times.Once);
    }

    [Fact]
    public async Task GetById_WhenAmenityDoesNotExist_ReturnsNotFound()
    {
        _amenityServiceMock
            .Setup(service => service.GetByIdAsync(99))
            .ReturnsAsync((AmenityDto?)null);

        var result = await _controller.GetById(99);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(notFound.Value);
        Assert.False(response.Success);
        Assert.Equal("Amenity not found.", response.Message);
        _amenityServiceMock.Verify(service => service.GetByIdAsync(99), Times.Once);
    }

    [Fact]
    public async Task Update_WhenAmenityDoesNotExist_ReturnsNotFound()
    {
        var dto = new AmenityCreateDto { Name = "Gym" };
        _amenityServiceMock
            .Setup(service => service.UpdateAsync(99, dto))
            .ReturnsAsync(false);

        var result = await _controller.Update(99, dto);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(notFound.Value);
        Assert.False(response.Success);
        Assert.Equal("Amenity not found.", response.Message);
        _amenityServiceMock.Verify(service => service.UpdateAsync(99, dto), Times.Once);
    }

    [Fact]
    public async Task Delete_WhenAmenityDoesNotExist_ReturnsNotFound()
    {
        _amenityServiceMock
            .Setup(service => service.DeleteAsync(99))
            .ReturnsAsync(false);

        var result = await _controller.Delete(99);

        var notFound = Assert.IsType<NotFoundObjectResult>(result);
        var response = Assert.IsType<ApiResponse<object>>(notFound.Value);
        Assert.False(response.Success);
        Assert.Equal("Amenity not found.", response.Message);
        _amenityServiceMock.Verify(service => service.DeleteAsync(99), Times.Once);
    }

    [Fact]
    public async Task Create_WhenAmenityIsInvalid_ThrowsValidationException()
    {
        var dto = new AmenityCreateDto();
        _amenityServiceMock
            .Setup(service => service.CreateAsync(dto))
            .ThrowsAsync(new ValidationException("Amenity name is required."));

        await Assert.ThrowsAsync<ValidationException>(() => _controller.Create(dto));
        _amenityServiceMock.Verify(service => service.CreateAsync(dto), Times.Once);
    }
}
