using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Core.Entity.App;

namespace TestApp.Web.Models.Authentication.Cookie
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly UserManager<User> _userManager;

        public CustomCookieAuthenticationEvents(UserManager<User> userManager)
        {
            // Get the database from registered DI services.
            _userManager = userManager;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;

            // Look for the LastChanged claim.
            var lastChanged = (from c in userPrincipal.Claims
                               where c.Type == "LastChanged"
                               select c.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(lastChanged)
                //|| !_userManager.ValidateLastChanged(lastChanged)
                )
            {
                context.RejectPrincipal();
                //want to non-destructively update the user principal
                context.ReplacePrincipal(new System.Security.Claims.ClaimsPrincipal(context.HttpContext.User.Identity));
                //and set the context.ShouldRenew property to true
                context.ShouldRenew = true;
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
