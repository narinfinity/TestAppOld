namespace TestApp.Core.Entity.Domain
{
    public class OrderedProduct : IEntity<int>
    {
        public int Id { get; set; }
        public int Count { get; set; }

        //relationships
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
