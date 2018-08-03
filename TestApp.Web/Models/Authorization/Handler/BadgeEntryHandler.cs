using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using TestApp.Web.Models.Authorization.Requirement;

namespace TestApp.Web.Models.Authorization.Handler
{
    public class BadgeEntryHandler : AuthorizationHandler<EnterBuildingRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EnterBuildingRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Name &&
                                           c.Issuer == "http://microsoftsecurity"))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
