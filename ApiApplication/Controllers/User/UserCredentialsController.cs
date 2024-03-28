using System.Diagnostics;
using ApiApplication.Controllers.User.Models;
using Authentication.Helpers;
using CommonInterfaces.Services;
using CommonInterfaces.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers.User;

[ApiController]
[Route("api/user/[controller]")]
public class UserCredentialsController(ISecurityUserService userService, IContextProvider contextProvider) : ControllerBase
{
    [Authorize<string>]
    [HttpGet]
    public UserCredentialsResponse Get()
    {
        var context = contextProvider.GetApplicationContext();
        if (!context.UserId.HasValue)
        {
            throw new Exception("User does not exist");
        }
        var userId = context.UserId.Value;

        var user = userService.GetById(userId)!;

        return new UserCredentialsResponse(user);
    }
}