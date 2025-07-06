using Azure;
using MCCMWebServiceApp.Common.Utilities;
using MCCMWebServiceApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Web.Bean;

namespace MCCMWebServiceApp.Web.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class UserTestController : BaseController<UserTestController>
    {
        private readonly IUserTestService _userTestService;

        public UserTestController(
           IUserTestService userTestService,
           ILogger<UserTestController> logger,
           IOptions<AppParameter> options)
           : base(options, logger)
        {
            _userTestService = userTestService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
            try
            {
                var user = await _userTestService.Authenticate(loginRequest.username, loginRequest.password);

                if (user == null)
                {
                    return Ok(new LoginResponse
                    {
                        Success = false,
                        Message = "ログインに失敗しました",
                        UserId = 0,
                        Username = null,
                        Email = null, 
                        CreateDate = DateTime.Now
                    });
                }

                return Ok(new LoginResponse
                {
                    Success = true,
                    Message = "ログインできました",
                    UserId = user.id,
                    Username = user.username,
                    Email = user.email,
                    CreateDate = user.createDate
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new BaseResponse
                {
                    Success = false,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new BaseResponse
                {
                    Success = false,
                    Message = $"ログインに失敗しました: {ex.Message}"
                });
            }
        }


        [HttpGet]
        public string TestLog4net()
        {
            _logger.LogInformation($"appParameter aaa is  {_appParameter.appParameter}");
            _logger.LogDebug("LogDebug");
            _logger.LogWarning("LogWarning");
            _logger.LogError(new Exception("LogError"), "LogError");
            return "ok";

        }

    }
}
