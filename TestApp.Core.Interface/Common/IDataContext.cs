using System.Linq;

namespace TestApp.Core.Interface.Common
{
    public interface IDataContext
    {
        IQueryable<TEntity> GetSet<TEntity>() where TEntity : class;
        void Attach<TEntity>(TEntity entity) where TEntity : class;
        void Detach<TEntity>(TEntity entity) where TEntity : class;
        TEntity Create<TEntity>(TEntity entity = null) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        void Save();
    }
}
