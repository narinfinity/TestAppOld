using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TestApp.Web
{
  public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                   // var context = services.GetRequiredService<SchoolContext>();
                   // DbInitializer.Initialize(context);
                }
                catch (System.Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
             WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseKestrel(options =>
                {                    
                    //options.Listen(IPAddress.Loopback, 44397, listenOptions =>
                    //{
                    //    listenOptions.UseHttps("certificate.pfx", "mypassword");
                    //});
                })
                .UseIISIntegration()
                .UseApplicationInsights()
                .Build();
    }
}
