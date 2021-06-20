using Data.Entities.Interfaces;

namespace Data.Entities
{
    public class BaseEntity : IEntity<long>
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public long Id { get; set; }
    }

    public class BaseEntity<T> : IEntity<T>
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public T Id { get; set; }
    }
}