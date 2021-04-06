using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace TrueLayerChallenge.WebApi.Filters
{
    public class ApiExceptionFilter: ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            _logger.LogError(exception, "");

#if !DEBUG
            var result = new
            {
                message = "An unhandled error occured",
            };
#else
            var result = new
            {
                message = exception.Message,
                stack = exception.StackTrace,
            };
#endif

            context.Exception = null;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new JsonResult(result);

            base.OnException(context);
        }
    }
}
