using System.Collections.Generic;
using TestApp.Core.Entity.Domain;

namespace TestApp.Core.Interface.Service.Domain
{
    public interface IProductStoreService
    {
        IEnumerable<Product> GetProductsByCategory(Category category);
        IEnumerable<Category> GetCategoryOrList(int id);
    }
}
