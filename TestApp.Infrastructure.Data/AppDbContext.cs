using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TestApp.Core.Entity;
using TestApp.Core.Entity.App;
using TestApp.Core.Entity.Domain;
using TestApp.Core.Interface.Common;
using TestApp.Infrastructure.Data.EntityConfiguration;
using Microsoft.AspNetCore.Identity;

namespace TestApp.Infrastructure.Data
{
  public class AppDbContext : IdentityDbContext<User, IdentityRole<string>, string, 
        IdentityUserClaim<string>, 
        IdentityUserRole<string>,
        IdentityUserLogin<string>, 
        IdentityRoleClaim<string>, 
        IdentityUserToken<string>>, IDataContext
    {
        static AppDbContext() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // Add your customizations after calling base.OnModelCreating(builder);

            //builder.UseOpenIddict<string>();
            builder.AddEntityConfigurations();
        }
        //DbSets
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderedProduct> OrderedProducts { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public IQueryable<TEntity> GetSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public new void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Attach(entity);
            Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void Detach<TEntity>(TEntity entity) where TEntity : class
        {
            if (Set<TEntity>().Local.Contains(entity))
            {
                Entry<TEntity>(entity).State = EntityState.Detached;
            }
        }

        public TEntity Create<TEntity>(TEntity entity = null) where TEntity : class
        {
            //if (entity != null)
            //{
            //    Set<TEntity>().Attach(entity);
            //    Entry<TEntity>(entity).State = EntityState.Added;
            //}
            //else
            {
                entity = Set<TEntity>().Add(entity).Entity;
                Entry<TEntity>(entity).State = EntityState.Added;
            }

            return entity;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            Set<TEntity>().Remove(entity);
        }

        public void Save()
        {
            var entities = ChangeTracker.Entries().Where(p => p.State == EntityState.Modified || p.State == EntityState.Added);

            foreach (var entityEntry in entities)
            {
                var entityInt = entityEntry.Entity as IEntity<int>;
                if (entityInt != null)
                {
                    entityEntry.State = entityInt.Id > 0 ? EntityState.Modified : EntityState.Added;
                }
                var entity = entityEntry.Entity as IdentityUser<string>;
                if (entity != null)
                {
                    //entityEntry.State = entity.Id == null ? EntityState.Added : EntityState.Modified;
                }

            }
            SaveChanges(acceptAllChangesOnSuccess: true);
        }


        protected virtual void Dispose(bool disposing)
        {
            //Clean up managable resources
            if (disposing)
            {

            }
            //Clean up unmanagable resources

            base.Dispose();
        }
        ~AppDbContext()
        {
            Dispose(false);
        }
    }
}
