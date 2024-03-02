using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Repositories;

namespace ApiApplication.Controllers;

[Route("api/[controller]")]
public class UserController(UserRepository userRepository) : Controller
{
    [HttpGet(Name = "GetUsers")]
    public List<Guid> Get()
    {
        return userRepository.GetAll().Select(user => user.Id).ToList();
    }
}