using System.Linq;
using Microsoft.AspNetCore.Identity;


namespace TestApp.Infrastructure.Data
{
  public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureCreated();
            // Look for any students.
            if (context.Products.Any())
            {
                return;   // DB has been seeded
            }

            var roles = new[] {
                new IdentityRole<string>("User"),
                new IdentityRole<string>("Admin")
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();

        }
    }
}
