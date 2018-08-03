using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using TestApp.Web.Models.Authorization.Requirement;

namespace TestApp.Web.Models.Authorization.Handler
{
    public class HasTemporaryStickerHandler : AuthorizationHandler<EnterBuildingRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EnterBuildingRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Email &&
                                           c.Issuer == "https://microsoftsecurity"))
            {
                // We'd also check the expiration date on the sticker.
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
