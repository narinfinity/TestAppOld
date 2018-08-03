using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TestApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using OpenIddict.Core;
using System.Threading;
using OpenIddict.Models;

namespace TestApp.Infrastructure.Dependency
{
  public static class DependencyServiceExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            return services.AddDbContext<AppDbContext>(optionsAction);
        }
        public static IdentityBuilder AddEFStores(this IdentityBuilder builder)
        {
            return builder.AddEntityFrameworkStores<AppDbContext>();
        }
        public static OpenIddictBuilder AddEFCoreStores(OpenIddictBuilder builder)
        {
            return builder.AddEntityFrameworkCoreStores<AppDbContext>();
        }
        public static IServiceProvider AddExternalDependencyServiceProvider(this IServiceCollection services, out IContainer container)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<DefaultRegistrationModule>();
            containerBuilder.Populate(services);
            container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        public async static Task InitializeAsync(this IServiceProvider services, CancellationToken cancellationToken)
        {
            // Create a new service scope to ensure the database context is correctly disposed when this methods returns.
            using (var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await context.Database.EnsureCreatedAsync();

                var manager = scope.ServiceProvider.GetRequiredService<OpenIddictApplicationManager<OpenIddictApplication<string>>>();

                if (await manager.FindByClientIdAsync("mvc", cancellationToken) == null)
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "mvc",
                        ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                        DisplayName = "MVC client application",
                        PostLogoutRedirectUris = { new Uri("http://localhost:44397/signout-callback-oidc") },
                        RedirectUris = { new Uri("http://localhost:44397/signin-oidc") }
                    };

                    await manager.CreateAsync(descriptor, cancellationToken);
                }

                // To test this sample with Postman, use the following settings:
                /**
                    GET: Authorization URL: https://localhost:44397/connect/authorize?
                    Headers: Accept: application/json;charset=UTF-8
                    POST: Authorization URL: https://localhost:44397/connect/authorize
                    Headers: Content-Type: application/x-www-form-urlencoded

                    response_type=code
                    &client_id=s6BhdRkqt3
                    &redirect_uri=http://https://localhost:44397/concent_page_uri
                    &scope=openid%20email%20profile%20roles%20offline_access
                    &state=af0ifjsldkj   // (CSRF, XSRF) mitigation by cryptographically binding it with cookie

                    Response: 
                **/
                /**
                    POST: Access token URL: http://localhost:44397/connect/token
                    grant_type=authorization_code
                    &code=request_id_from_authz_server
                    &client_id=public_client
                    &client_secret=[blank] (not used with public clients)
                    &scope=openid%20email%20profile%20roles%20offline_access

                    * Client ID: public_client
                    * Client secret: [blank] (not used with public clients)
                    * Scope: openid email profile roles
                    * 
                    * Request access token locally: yes
                **/
                if (await manager.FindByClientIdAsync("s6BhdRkqt3", cancellationToken) == null)
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "s6BhdRkqt3",
                        DisplayName = "public_client",
                        RedirectUris = { new Uri("https://www.getpostman.com/oauth2/callback") }
                    };

                    await manager.CreateAsync(descriptor, cancellationToken);
                }
            }
        }


    }
}
