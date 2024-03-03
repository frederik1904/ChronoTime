using ApiApplication.Controllers.User.Models;
using Authentication.Helpers;
using CommonInterfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers.User;

[ApiController]
[Route("api/user/[controller]")]
public class UserCredentialsController(IUserService userService) : ControllerBase
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