using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using Taxually.Application.Exceptions;
using Taxually.TechnicalTest.Models;

namespace Taxually.TechnicalTest.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await handleexceptionAsync(context, e);
                throw;
            }
        }

        private async Task handleexceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            CustomProblemDetails problem = new();

            switch (ex)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problem = new CustomProblemDetails
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Detail = badRequestException.InnerException?.Message,
                        Type = nameof(BadRequestException),
                        Errors = badRequestException.ValidationErrors
                    };
                    break;
                default:
                    problem = new CustomProblemDetails
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.StackTrace,
                    };
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            var problemDetailsJson = JsonConvert.SerializeObject(problem);
            _logger.LogError(problemDetailsJson);

            context.Response.ContentType = "application/problem+json";
            
            await context.Response.WriteAsync(problemDetailsJson);
            
        }
    }

}
