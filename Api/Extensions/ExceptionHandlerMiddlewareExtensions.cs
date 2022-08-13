using Domain.Constant;
using Domain.DTO;
using Domain.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace Api.Extensions
{
    public static class ExceptionHandlerMiddlewareExtensions
    {
        private static readonly Dictionary<HttpStatusCode, ProblemDetailsDto> _errosReponses = new Dictionary<HttpStatusCode, ProblemDetailsDto>
        {
            {
                HttpStatusCode.BadRequest,
                new ProblemDetailsDto() {
                    Title = ExceptionExtensionConstant.BAD_REQUEST_TITLE,
                    Status = HttpStatusCode.BadRequest
                }
            },
            {
                HttpStatusCode.NotFound,
                new ProblemDetailsDto() {
                    Title = ExceptionExtensionConstant.NOT_FOUND_TITLE,
                    Status = HttpStatusCode.NotFound
                }
            }
        };

        public static void UseExceptionHandlerMiddleware(this IApplicationBuilder application, ILoggerFactory loggerFactory)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                    RegisterLog(context, loggerFactory, exceptionHandlerPathFeature);

                    ProblemDetailsDto errorResponse = GetMessageStatusCode(((DefaultException)exceptionHandlerPathFeature.Error).StatusCode);
                    errorResponse.Errors = ((DefaultException)exceptionHandlerPathFeature.Error).Message;
                    errorResponse.Instance = context.Request.HttpContext.Request.Path;

                    context.Response.StatusCode = (int)((DefaultException)exceptionHandlerPathFeature.Error).StatusCode;
                    context.Response.ContentType = "application/problem+json";
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
                });
            });
        }

        private static void RegisterLog(HttpContext context, ILoggerFactory loggerFactory, IExceptionHandlerPathFeature exceptionHandlerPathFeature)
        {
            ILogger logger = loggerFactory.CreateLogger(exceptionHandlerPathFeature.Error.GetType());

            if (exceptionHandlerPathFeature.Error is DefaultException defaultException && logger.IsEnabled(defaultException.Level))
            {
                logger.Log(logLevel: defaultException.Level,
                           exception: defaultException,
                           message: exceptionHandlerPathFeature.Error.Message);
            }
            else
            {
                logger.Log(LogLevel.Error, exceptionHandlerPathFeature.Error, exceptionHandlerPathFeature.Error.Message);
            }
        }

        private static ProblemDetailsDto GetMessageStatusCode(HttpStatusCode httpStatusCode)
        {
            _errosReponses.TryGetValue(httpStatusCode, out ProblemDetailsDto errorResponse);
            if (errorResponse == null)
            {
                _errosReponses.TryGetValue(HttpStatusCode.InternalServerError, out errorResponse);
            }
            return errorResponse;
        }
    }
}