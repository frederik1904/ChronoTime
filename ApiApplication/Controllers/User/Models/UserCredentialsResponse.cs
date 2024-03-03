using CommonInterfaces.Models;

namespace ApiApplication.Controllers.User.Models;

public class UserCredentialsResponse(Guid id, string email, string username)
{
    public Guid Id { get; set; } = id;
    public string Email { get; set; } = email;
    public string Username { get; set; } = username;

    public UserCredentialsResponse(IUser user) : this(user.Id, user.Email, user.Username)
    {
    }
}