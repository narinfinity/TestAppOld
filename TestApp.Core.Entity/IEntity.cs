namespace TestApp.Core.Entity
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
