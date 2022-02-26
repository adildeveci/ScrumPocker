using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using ScrumPocker.Core.Models.BaseResponse;
using System.Text.Json;

namespace ScrumPocker.Core.Extensions
{
    public static class CustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (errorFeature != null)
                    {
                        var ex = errorFeature.Error;

                        ErrorDto errorDto = null;

                        //ozel exceptionlar firlatilip burada farkli sekilde handle edilebilir
                        //if (ex is Exceptions.CustomException)
                        //{
                        //    errorDto = new ErrorDto(ex.Message);
                        //}
                        //else
                        //{
                        errorDto = new ErrorDto(ex.Message);
                        //}

                        var response = BaseResponse.Fail(errorDto, 500);

                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                });
            });
        }
    }
}