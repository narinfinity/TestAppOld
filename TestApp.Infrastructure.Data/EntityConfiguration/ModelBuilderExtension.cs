using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TestApp.Core.Entity.App;
using TestApp.Core.Entity.Domain;

namespace TestApp.Infrastructure.Data.EntityConfiguration
{
    public static class ModelBuilderExtension
    {
        public static void AddEntityConfigurations(this ModelBuilder builder)
        {
            //ASP.NET Identity
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            builder.Entity<User>(b =>
            {
                b.ToTable("Users");
                b.Property(e => e.Id).HasMaxLength(128).IsUnicode(false).IsRequired();
                b.Property(e => e.ConcurrencyStamp).HasMaxLength(128).IsUnicode(false).IsRequired(false);
                b.Property(e => e.FirstName).HasMaxLength(100).IsUnicode().IsRequired();
                b.Property(e => e.LastName).HasMaxLength(100).IsUnicode().IsRequired();
                b.Property(e => e.Email).HasMaxLength(100).IsUnicode(false).IsRequired();
                b.Property(e => e.NormalizedEmail).HasMaxLength(100).IsUnicode(false).IsRequired(false);
                b.Property(e => e.UserName).HasMaxLength(100).IsUnicode().IsRequired();
                b.Property(e => e.NormalizedUserName).HasMaxLength(256).IsUnicode();
                b.Property(e => e.PasswordHash).HasMaxLength(256).IsUnicode(false).IsRequired(false);
                b.Property(e => e.SecurityStamp).HasMaxLength(256).IsUnicode(false).IsRequired(false);
                b.Property(e => e.PhoneNumber).HasMaxLength(100).IsUnicode(false).IsRequired(false);

                b.HasKey(e => e.Id);
                b.HasIndex(e => e.NormalizedUserName).IsUnique().HasName("UserNameIndex");
                b.HasIndex(e => e.NormalizedEmail).HasName("EmailIndex");
            });
            builder.Entity<User>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<IdentityRole<string>>(b =>
            {
                b.ToTable("Roles");

                b.Property(e => e.Id).HasMaxLength(128).IsUnicode(false).ValueGeneratedOnAdd();
                b.Property(e => e.ConcurrencyStamp).HasMaxLength(128).IsUnicode(false).IsConcurrencyToken();
                b.Property(e => e.Name).HasMaxLength(128).IsUnicode(false);
                b.Property(e => e.NormalizedName).HasMaxLength(128).IsUnicode(false);
                b.Property<string>("Discriminator").HasMaxLength(128).IsUnicode(false).IsRequired(false);

                b.HasKey(e => e.Id);
                b.HasIndex(e => e.NormalizedName).IsUnique().HasName("RoleNameIndex");
            });

            //Domain EntityConfiguration
            builder.Entity<Product>(b =>
            {
                b.ToTable("Products");

                b.Property(e => e.Id).ValueGeneratedOnAdd();
                b.Property(e => e.Name).HasMaxLength(1024).IsUnicode().IsRequired();
                b.Property(e => e.Description).HasMaxLength(4000).IsUnicode().IsRequired(false);
                b.Property(e => e.Url).HasMaxLength(2048).IsUnicode(false).IsRequired(false);
                b.Property(e => e.Price).IsRequired();

                b.HasKey(e => e.Id);
                b.HasOne(e => e.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId);
            });
            builder.Entity<Category>(b =>
            {
                b.ToTable("Categories");

                b.Property(e => e.Id).ValueGeneratedOnAdd();
                b.Property(e => e.Name).HasMaxLength(128).IsUnicode().IsRequired();

                b.HasKey(e => e.Id);
            });
            builder.Entity<OrderedProduct>(b =>
            {
                b.ToTable("OrderedProducts");

                b.Property(e => e.Id).ValueGeneratedOnAdd();
                b.Property(e => e.Count).IsRequired();

                b.HasKey(e => e.Id);
                b.HasOne(e => e.Product).WithMany().HasForeignKey("ProductId");
                b.HasOne(e => e.Order).WithMany(e => e.OrderedProducts).HasForeignKey(e => e.OrderId);
            });
            builder.Entity<Order>(b =>
            {
                b.ToTable("Orders");

                b.Property(e => e.Id).ValueGeneratedOnAdd();
                b.Property(e => e.OrderedDate).IsRequired();
                b.Property(e => e.Total).IsRequired();

                b.HasKey(e => e.Id);
                b.HasOne(e => e.User).WithMany();
                b.HasOne(e => e.Address).WithMany();
            });
            builder.Entity<Address>(b =>
            {
                b.ToTable("Addresses");

                b.Property(e => e.Id).ValueGeneratedOnAdd();
                b.Property(e => e.Street).HasMaxLength(128).IsUnicode().IsRequired();
                b.Property(e => e.City).HasMaxLength(128).IsUnicode().IsRequired();
                b.Property(e => e.State).HasMaxLength(128).IsUnicode().IsRequired(false);
                b.Property(e => e.Country).HasMaxLength(128).IsUnicode().IsRequired();
                b.Property(e => e.PostalCode).HasMaxLength(128).IsUnicode().IsRequired(false);

                b.HasKey(e => e.Id);
            });


        }
    }
}
