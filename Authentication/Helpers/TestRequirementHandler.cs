using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Helpers;

public class TestRequirementHandler: AuthorizationHandler<TestRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TestRequirement requirement)
    {
        var user = (string)((DefaultHttpContext)context.Resource).Items["User"];
        if (!string.IsNullOrEmpty(user))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}