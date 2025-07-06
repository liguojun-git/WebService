using Microsoft.AspNetCore.Mvc.Filters;

namespace MCCMWebServiceApp.Web.Filter
{
    public class LogFilter : IActionFilter
    {
        private readonly ILogger<LogFilter> _logger;

        public LogFilter(ILogger<LogFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // 记录请求开始日志
            _logger.LogInformation($"実行開始です {context.ActionDescriptor.DisplayName}");

            // 记录请求参数
            foreach (var argument in context.ActionArguments)
            {
                _logger.LogDebug($"パラメータです {argument.Key}: {argument.Value}");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                // 记录异常
                _logger.LogError(context.Exception, $"実行します {context.ActionDescriptor.DisplayName} 異常が発生します");
            }
            else
            {
                // 记录正常完成
                _logger.LogInformation($"実行完了です。 {context.ActionDescriptor.DisplayName}");
            }
        }
    }
}