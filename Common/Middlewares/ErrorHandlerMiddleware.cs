using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Common.Exceptions;

namespace Common.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            HttpStatusCode status;
            var stackTrace = string.Empty;
            var data = (object) null;

            switch (exception)
            {
                case NotFoundException e:
                    status = HttpStatusCode.NotFound;
                    data = e.Data;
                    break;
                case ForbiddenException e:
                    status = HttpStatusCode.Forbidden;
                    data = e.Data;
                    break;
                case AuthorizationException e:
                    status = HttpStatusCode.Unauthorized;
                    data = e.Data;
                    break;
                default:
                    status = HttpStatusCode.InternalServerError;
                    stackTrace = exception.StackTrace;
                    break;
            }

            var result = JsonSerializer.Serialize(new
            {
                error = exception?.Message,
                stackTrace,
                data
            }, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });


            response.StatusCode = (int) status;
            return response.WriteAsync(result);
        }
    }
}
