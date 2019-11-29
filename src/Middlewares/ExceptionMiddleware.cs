﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Breadloaf.Middlewares {
    public readonly struct ExceptionMiddleware {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ExceptionMiddleware> logger) {
            try {
                await _next(context);
            }
            catch (Exception ex) {
                HandleException(ex, logger, context);
            }
        }

        private void HandleException(Exception ex, ILogger logger, HttpContext context) {
            logger.Log(LogLevel.Error, ex.Message, ex);
            context.Response.StatusCode = 500;
        }
    }
}