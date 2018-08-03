using System;
using System.Linq;
using System.Linq.Expressions;
using TestApp.Core.Entity.Domain;
using TestApp.Core.Interface.Common;

namespace TestApp.Core.Interface.Repository
{
    public interface IProductRepository : IRepository<Product, int>
    {        
        IQueryable<Product> GetListWithUser(Expression<Func<Product, bool>> predicate = null);
    }
}
