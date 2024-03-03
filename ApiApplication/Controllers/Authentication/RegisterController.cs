using ApiApplication.Controllers.Authentication.Models;
using Common.Models.Validation;
using CommonInterfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers.Authentication;

[ApiController]
[Route("api/auth/[controller]")]
public class RegisterController(IUserService userService, ITransactionService transactionService) : ControllerBase
{

    [HttpPost]
    public ActionResult<RegisterResponse> Post(RegisterRequest request)
    {
        return transactionService.Transactional<ActionResult<RegisterResponse>>(() =>
        {
            var validatedApplicant =
                ValidatedUserApplicant.CreateValidatedApplicant(request.Email, request.Username,
                    request.Password.ExposeSecret());
            if (validatedApplicant.DidValidationFail()) return BadRequest(validatedApplicant.ErrorMsg);

            if (validatedApplicant.ValidatedObject == null)
                return BadRequest(new List<string> { "Validated object is empty" });

            var result = userService.RegisterUser(validatedApplicant.ValidatedObject);

            if (result == null) return BadRequest("User could not be generated");

            return Ok(new RegisterResponse(result));
        });
    }
}