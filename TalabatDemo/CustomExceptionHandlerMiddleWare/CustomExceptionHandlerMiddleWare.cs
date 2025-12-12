using Azure;
using Domain.Execptions;
using Microsoft.AspNetCore.Http;
using Shared.Error_Models;

namespace TalabatDemo.CustomMiddleWare
{
    public class CustomExceptionHandlerMiddleWare
    {
       
            private readonly RequestDelegate _next;
            private readonly ILogger<CustomExceptionHandlerMiddleWare> _logger;
            public CustomExceptionHandlerMiddleWare(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleWare> Logger)
            {
                _next = next; //next Middleware in the Pipeline
            _logger = Logger; //catch errors
            }
            public async Task InvokeAsync(HttpContext Httpcontext)
            {
                try
            {
                await _next.Invoke(Httpcontext);
                await EndPointNotFoundException(Httpcontext);

            }
            catch (Exception ex) //Global Exception Handler Can see all Exceptions that inherit from Exception Class
            {
                _logger.LogError(ex, "Something Went Wrong");
                //Httpcontext.Response.StatusCode = StatusCodes.Status500InternalServerError;//status code = 500 always and this is not true for all exceptions
               
                await HandleExceptionAsync(Httpcontext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext Httpcontext, Exception ex)
        {
            Httpcontext.Response.ContentType = "application/json";
            var response = new ErrorToReturn()
            {
               
                ErrorMessage = ex.Message,
            };
            Httpcontext.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnAuthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException => GetBadRequestErrors(badRequestException, response),
                _ => StatusCodes.Status500InternalServerError
            };
            response.StatusCode = Httpcontext.Response.StatusCode;
            var jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);
            await Httpcontext.Response.WriteAsync(jsonResponse);
        }
        private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn response)
        {
            response.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }
        private static async Task EndPointNotFoundException(HttpContext Httpcontext)
        {
            if (Httpcontext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var response = new ErrorToReturn()
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {Httpcontext.Request.Path} is Not Found"
                };
                await Httpcontext.Response.WriteAsJsonAsync(response);
            }
        }

       
    }
    }

