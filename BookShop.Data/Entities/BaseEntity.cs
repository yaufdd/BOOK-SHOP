using Data.Entities.Interfaces;

namespace Data.Entities
{
    public class BaseEntity : IEntity<long>
    {
        public long Id { get; set; }
    }

    public class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}