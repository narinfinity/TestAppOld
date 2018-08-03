using Autofac;
using TestApp.Core.Interface.Common;
using TestApp.Core.Interface.Service.App;
using TestApp.Core.Interface.Service.Domain;
using TestApp.Infrastructure.Data;
using TestApp.Infrastructure.Data.Common;
using TestApp.Service.App;
using TestApp.Service.Domain;

namespace TestApp.Infrastructure.Dependency
{
    public class DefaultRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppDbContext>().InstancePerLifetimeScope().As<IDataContext>();
            builder.RegisterType<UnitOfWork>().InstancePerLifetimeScope().As<IUnitOfWork>();
            //Domain services
            builder.RegisterType<ProductStoreService>().InstancePerLifetimeScope().As<IProductStoreService>();
            builder.RegisterType<ShoppingService>().InstancePerLifetimeScope().As<IShoppingService>();
            //builder.RegisterType<DocumentRepository>().InstancePerLifetimeScope().As<IDocumentRepository>();

            //app services
            builder.RegisterType<AuthMessageSender>().InstancePerLifetimeScope().As<IEmailSender>();
            builder.RegisterType<AuthMessageSender>().InstancePerLifetimeScope().As<ISmsSender>();

        }
    }
}
