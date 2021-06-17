namespace Data.Entities.Interfaces
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}