using System.Collections.Generic;
using TestApp.Core.Entity.Domain;

namespace TestApp.Core.Interface.Service.Domain
{
   public interface IShoppingService
    {
        IEnumerable<Order> GetOrderOrList(int id, string userId);
        Order SaveOrder(Order order);
    }
}
