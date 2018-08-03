using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TestApp.Web.Models.Authorization.Requirement;

namespace TestApp.Web.Models.Authorization.Handler
{
    public class LoggingAuthorizationHandler : AuthorizationHandler<MyLoggingAuthorizationRequirement>
    {
        ILogger _logger;

        public LoggingAuthorizationHandler(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger(this.GetType().FullName);
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MyLoggingAuthorizationRequirement requirement)
        {
            _logger.LogInformation("Inside my handler");

            context.Succeed(requirement);
            // Check if the requirement is fulfilled.
            return Task.CompletedTask;
        }
    }
}
