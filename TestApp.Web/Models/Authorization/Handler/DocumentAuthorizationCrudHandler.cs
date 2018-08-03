using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;
using TestApp.Core.Entity.Domain;
using TestApp.Web.Models.Authorization.Requirement.Operation;

namespace TestApp.Web.Models.Authorization.Handler
{
    public class DocumentAuthorizationCrudHandler : AuthorizationHandler<OperationAuthorizationRequirement, Document>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       OperationAuthorizationRequirement requirement,
                                                       Document resource)
        {
            if (context.User.Identity?.Name == resource.Author && requirement.Name == CrudOperationRequirement.Read.Name)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
