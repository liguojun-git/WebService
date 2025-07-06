using MCCMWebServiceApp.Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MCCMWebServiceApp.Web.Controllers;

public class BaseController<TLogger> : ControllerBase where TLogger : class
{
    protected readonly AppParameter _appParameter;
    protected readonly ILogger<TLogger> _logger;

    public BaseController(IOptions<AppParameter> options, ILogger<TLogger> logger)
    {
        _appParameter = options.Value;
        _logger = logger;
    }
}

