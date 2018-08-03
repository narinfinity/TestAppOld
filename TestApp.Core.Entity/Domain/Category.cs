using System.Collections.Generic;

namespace TestApp.Core.Entity.Domain
{
    public class Category : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //relationships
        public ICollection<Product> Products { get; set; }
    }
}
