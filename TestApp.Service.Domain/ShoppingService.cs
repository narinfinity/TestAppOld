using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TestApp.Core.Entity.Domain;
using TestApp.Core.Interface.Common;
using TestApp.Core.Interface.Service.Domain;

namespace TestApp.Service.Domain
{
    public class ShoppingService : IShoppingService, IDisposable
    {
        IUnitOfWork _unitOfWork;
        public ShoppingService(IUnitOfWork unitOfWork)
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
        public IEnumerable<Order> GetOrderOrList(int id, string userId)
        {
            var includeUsefulProps = new List<Expression<Func<Order, object>>> { e => e.OrderedProducts };
            return _unitOfWork.Repository<Order, int>().GetList(e => e.UserId == userId && (id == 0 || e.Id == id), o => o.OrderByDescending(e => e.OrderedDate), includeUsefulProps);
        }

        public Order SaveOrder(Order order)
        {
            order.Total = order.OrderedProducts.Select(op => new { Price = op.Product.Price, Count = op.Count }).Sum(e => e.Price * e.Count);
            order.OrderedDate = DateTime.Now;

            foreach (var op in order.OrderedProducts)
            {
                op.Order = order;
            }

            _unitOfWork.Repository<Order, int>().Create(order);
            _unitOfWork.Save();

            return order;
        }
    }
}
