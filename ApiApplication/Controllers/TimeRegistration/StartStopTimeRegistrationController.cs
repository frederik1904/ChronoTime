using Authentication.Helpers;
using CommonInterfaces.Services;
using CommonInterfaces.Services.Authentication;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Repositories;
using WorkflowApplication;

namespace ApiApplication.Controllers.TimeRegistration;

[ApiController]
[Route("api/auth/[controller]")]
public class StartStopTimeRegistrationController(IAuthentication authentication, IUserService userService) : Controller
{
    
    [Authorize<string>]
    [HttpPost]
    public ActionResult<string> Post()
    {
        var userId = (string)HttpContext.Items["User"]!;
        var user = userService.GetById(Guid.Parse(userId))!;

        var token = authentication.GenerateJwtToken(user);

        var channel = GrpcChannel.ForAddress("http://localhost:5050");
        var client = new Greeter.GreeterClient(channel);
        var headers = new Metadata();
        headers.Add("Authorization", $"Bearer {token}");
        var resp = client.SayHello(new HelloRequest(){Name = user.Username}, headers);
        
        return resp?.Message ?? "No response";
    }
}