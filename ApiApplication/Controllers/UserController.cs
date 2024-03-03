using System.Text.Json.Nodes;
using CommonInterfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Repositories;

namespace ApiApplication.Controllers;

[Route("api/[controller]")]
public class UserController(UserRepository userRepository, ITransactionService transactionService) : Controller
{
    [HttpGet(Name = "GetUsers")]
    public List<Guid> Get()
    {
        return transactionService.Transactional(() => userRepository.GetAll().Select(user => user.Id).ToList());
    }
}