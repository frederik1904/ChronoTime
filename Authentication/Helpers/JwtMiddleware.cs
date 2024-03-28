using CommonInterfaces.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CommonInterfaces.Models.Authentication;
using CommonInterfaces.Models.Database;
using CommonInterfaces.Services.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;


namespace Authentication.Helpers;

public class JwtMiddleware(IAppSettings appSettings, RequestDelegate next)
{
    public async Task Invoke(HttpContext context, ISecurityUserService userService, IContextProvider contextProvider)
    {
        var token = context.Request.Headers.Authorization.FirstOrDefault()?.Split(" ").Last();

        if (!string.IsNullOrEmpty(token))
            AttachUserToContext(context, userService, token, contextProvider);

        await next(context);
    }

    private void AttachUserToContext(HttpContext context, ISecurityUserService userService, string token, IContextProvider contextProvider)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.GetSecret().ExposeSecret());

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            // attach user to context on successful jwt validation
            var user = userService.GetById(userId);
            context.Items["User"] = userId.ToString();
            var identity = new ClaimsIdentity(new List<Claim>
            {
                new Claim("UserId", userId.ToString(), ClaimValueTypes.String)
            }, "CUSTOM");
            context.User = new ClaimsPrincipal(identity);
            SetContextProvider(user, contextProvider);
            // context.Items["CLAIM_TENANT_ID"] = user.GetTenantId().ToString();
        }
        catch (Exception e)
        {
            // do nothing if jwt validation fails
            // user is not attached to context so request won't have access to secure routes
        }
    }

    private void SetContextProvider(User? user, IContextProvider contextProvider)
    {
        if (user == null)
            return;
        contextProvider.SetApplicationContext(new ApplicationContext
        {
            UserId = user.GetId()
        });
    }
}