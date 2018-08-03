using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TestApp.Core.Entity.Domain;
using TestApp.Core.Interface.Common;
using TestApp.Core.Interface.Service.Domain;

namespace TestApp.Service.Domain
{
    public class ProductStoreService : IProductStoreService, IDisposable
    {
        IUnitOfWork _unitOfWork;
        public ProductStoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            //Clean up managable resources
            if (disposing)
            {
                if (_unitOfWork != null)
                {
                    _unitOfWork.Dispose();
                    _unitOfWork = null;
                }
            }
            //Clean up unmanagable resources

        }
        public virtual IEnumerable<Product> GetProductsByCategory(Category category)
        {
            var includeUsefulProps = new List<Expression<Func<Product, object>>> { e => e.Category };
            return _unitOfWork.Repository<Product, int>().GetList(e => category.Id == 0 || e.Category.Id == category.Id, o => o.OrderBy(e => e.Price), includeUsefulProps);
        }
        public virtual IEnumerable<Category> GetCategoryOrList(int id)
        {
            //var includeUsefulProps = new List<Expression<Func<Category, object>>> { };
            return _unitOfWork.Repository<Category, int>().GetList(e => id == 0 || e.Id == id, o => o.OrderBy(e => e.Name));
        }
    }
}
