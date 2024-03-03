using CommonInterfaces.Models;

namespace ApiApplication.Controllers.Authentication.Models;

public class LoginResponse
{
    public LoginResponse(IUser? user, string token)
    {
        Username = user.Username;
        Id = user.Id;
        Token = token;
    }

    public string Username { get; set; }
    public Guid Id { get; set; }
    public string Token { get; set; }
}