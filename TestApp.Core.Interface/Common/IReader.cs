using System.Linq;
using TestApp.Core.Entity;

namespace TestApp.Core.Interface.Common
{
    public interface IReader<out TEntity, in TKey>
       where TEntity : class, IEntity<TKey>
    {
        TEntity Get(TKey id, bool tracking = true);
        IQueryable<TEntity> GetList(bool tracking = true);
    }
}
