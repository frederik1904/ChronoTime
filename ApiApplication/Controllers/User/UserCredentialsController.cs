using ApiApplication.Controllers.User.Models;
using Authentication.Helpers;
using CommonInterfaces.Services;
using CommonInterfaces.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers.User;

[ApiController]
[Route("api/user/[controller]")]
public class UserCredentialsController(ISecurityUserService userService) : ControllerBase
{
    [Authorize<string>]
    [HttpGet]
    public UserCredentialsResponse Get()
    {
        var userId = (string)HttpContext.Items["User"]!;
        var user = userService.GetById(Guid.Parse(userId))!;

        return new UserCredentialsResponse(user);
    }
}