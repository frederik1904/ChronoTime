using CommonInterfaces.Models;

namespace ApiApplication.Controllers.Authentication.Models;

public class RegisterResponse
{
    public RegisterResponse(CommonInterfaces.Models.AUser? user)
    {
        Username = user.Username;
        Email = user.Email;
        Id = user.Id;
    }

    public string Username { get; set; }
    public string Email { get; set; }
    public Guid Id { get; set; }
}