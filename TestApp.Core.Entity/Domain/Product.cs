namespace TestApp.Core.Entity.Domain
{
   public class Product : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public decimal Price { get; set; }

        //relationships
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
