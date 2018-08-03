namespace TestApp.Core.Entity.Domain
{
  public class Document : IEntity<int>
    {
        public int Id { get; set; }
        public string Author { get; set; }
    }
}
