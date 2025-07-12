using System.Net;
using MCCMWebServiceApp.Web.Filter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.Filiter;

public class ExceptionFilter : IAsyncExceptionFilter
{
    private readonly ILogger<LogFilter> _Logger;

    public ExceptionFilter(ILogger<LogFilter> logger)
    {
        _Logger = logger;
    }

    public async Task OnExceptionAsync(ExceptionContext context)
    {
        _Logger.LogError(context.Exception, "未処理異常が発生します。");

        var staticCode = MapExtensionToHttpResponseCode(context.Exception);
        context.Result = new JsonResult(new
        {
            Type = context.Exception.GetType().Name,
            Message = context.Exception.Message,
            StackTrance = context.Exception.StackTrace
        })
        {
            StatusCode = (int)staticCode
        };
        context.ExceptionHandled = true;
        await Task.CompletedTask;
    }


    private HttpStatusCode MapExtensionToHttpResponseCode(Exception exception)
    {
        var result = exception switch
        {
            ArgumentNullException _ => (HttpStatusCode.BadRequest),
            ArgumentException _ => (HttpStatusCode.BadRequest),
            UnauthorizedAccessException _ => (HttpStatusCode.Unauthorized),
            NotImplementedException _ => (HttpStatusCode.NotImplemented),
            _ => (HttpStatusCode.InternalServerError)
        };
        return result;

    }




}

