using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ScrumPocker.Core.Models.BaseResponse;
using System.Linq;

namespace ScrumPocker.Core.Extensions
{
    public static class CustomValidationResponse
    {
        public static void UseCustomValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0).SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                    var errorDto = new ErrorDto(errors.ToList());

                    var response = BaseResponse.Fail(errorDto, 400);
                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}