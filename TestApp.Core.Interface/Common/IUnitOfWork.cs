using TestApp.Core.Entity;

namespace TestApp.Core.Interface.Common
{
    public interface IUnitOfWork
    {
        void Dispose();        
        void Save();
        IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class, IEntity<TKey>;
    }
}
