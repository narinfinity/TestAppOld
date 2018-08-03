using System;
using System.Collections.Generic;
using TestApp.Core.Entity.App;

namespace TestApp.Core.Entity.Domain
{
    public class Order : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime OrderedDate { get; set; }
        public decimal Total { get; set; }

        //relationships
        public ICollection<OrderedProduct> OrderedProducts { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int AddressId { get; set; }
        public Address Address { get; set; }

    }
}
