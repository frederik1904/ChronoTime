using ApiApplication.Controllers.Authentication.Models;
using CommonInterfaces.Services;
using CommonInterfaces.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers.Authentication;

[ApiController]
[Route("api/auth/[controller]")]
public class LoginController(ILogger<LoginController> logger, IUserService userService, IAuthentication authentication, ITransactionService transactionService)
    : ControllerBase
{
    private readonly ILogger<LoginController> _logger = logger;

    [HttpPost]
    public ActionResult<LoginResponse> Post(LoginRequest request)
    {
        return transactionService.Transactional<ActionResult<LoginResponse>>(() =>
        {
            var user = userService.GetUserByEmail(request.Email);

            if (user == null) return Unauthorized();

            var passwordMatch = userService.CheckIfPasswordsMatchAndUpgradeIfNeeded(user, request.Password);

            if (!passwordMatch) return Unauthorized();

            var token = authentication.GenerateJwtToken(user);

            return new LoginResponse(user, token);
        });
    }
}