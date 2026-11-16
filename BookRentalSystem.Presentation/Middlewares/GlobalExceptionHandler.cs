using BookRentalSystem.Business.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookRentalSystem.Presentation.Middlewares
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Unhandled exception occurred: {Message}", exception.Message);

            var (statusCode, title, detail) = exception switch
            {
                NotFoundException => (StatusCodes.Status404NotFound, "Resource Not Found", exception.Message),
                ConflictException => (StatusCodes.Status409Conflict, "Conflict", exception.Message),
                BadRequestException => (StatusCodes.Status400BadRequest, "Bad Request", exception.Message),
                UnauthorizedException => (StatusCodes.Status401Unauthorized, "Unauthorized", exception.Message),
                ForbiddenException => (StatusCodes.Status403Forbidden, "Forbidden", exception.Message),
                DbUpdateConcurrencyException => (StatusCodes.Status409Conflict, "Concurrency Conflict", "The record was modified concurrently by another operation. Please reload and try again."),
                DbUpdateException { InnerException: PostgresException { SqlState: PostgresErrorCodes.UniqueViolation } }
                    => (StatusCodes.Status409Conflict, "Conflict", "A record with the same value already exists."),
                _ => (StatusCodes.Status500InternalServerError, "Internal Server Error", "An unexpected error occurred.")
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
