using FluentValidation;
using HotelManagement.Common.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelManagement.API.Filters;

public class ValidationFilters<T>(IValidator<T> validator) : IAsyncActionFilter
{
    private readonly IValidator<T> _validator = validator;
    
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var model = context.ActionArguments.Values
            .OfType<T>()
            .FirstOrDefault();

        if (model is null)
        {
            await next();
            return;
        }
        
        var result = await _validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            var response = new ApiResponse<string?> 
            {
                Success = false,
                StatusCode = 400,
                Message = "Validation Failed.",
                Data = null,
                Errors = result.Errors.Select(x => $"{x.PropertyName} => {x.ErrorMessage}").ToList(),
                Timestamp = DateTime.UtcNow
            };
            context.Result = new BadRequestObjectResult(response);
            return;
        }
        
        await next();
    }
}
