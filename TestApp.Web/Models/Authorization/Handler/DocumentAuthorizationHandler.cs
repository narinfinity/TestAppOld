using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using TestApp.Core.Entity.Domain;
using TestApp.Web.Models.Authorization.Requirement;

namespace TestApp.Web.Models.Authorization.Handler
{
    public class DocumentAuthorizationHandler : AuthorizationHandler<SameAuthorRequirement, Document>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       SameAuthorRequirement requirement,
                                                       Document resource)
        {
            if (context.User.Identity?.Name == resource.Author)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
