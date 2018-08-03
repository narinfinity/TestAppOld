using Microsoft.AspNetCore.Authorization;

namespace TestApp.Web.Models.Authorization.Requirement
{
    public class HasScopeRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Scope { get; }

        public HasScopeRequirement(string scope, string issuer)
        {
            Scope = scope;
            Issuer = issuer;
        }
    }
}
