using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace ApiApplication.Controllers;

[Route("api/[controller]")]
public class UserController(ChronoContext chronoContext) : Controller
{
    [HttpGet(Name = "GetUsers")]
    public List<Guid> Get()
    {
        return chronoContext.Users.Select(user => user.Id).ToList();
    }
}