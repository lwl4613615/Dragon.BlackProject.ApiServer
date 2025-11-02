using Dragon.BlackProject.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Dragon.BlackProject.ApiServer.Utils.Filter
{
    /// <summary>
    /// 用作处理异常的
    /// </summary>
    public class CustomAsyncExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<CustomAsyncExceptionFilter> _logger;
        
        public CustomAsyncExceptionFilter(ILogger<CustomAsyncExceptionFilter> logger)
        {
            _logger = logger;
        }
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.ExceptionHandled==false)
            {
                _logger.LogError( $"捕获到未处理的异常,异常为{context.Exception.Message}");
                context.Result = new JsonResult(ApiResult<string>.Fail(extra:context.Exception.Message));
            }
            context.ExceptionHandled = true;
            await Task.CompletedTask;

        }
       

    }
}
