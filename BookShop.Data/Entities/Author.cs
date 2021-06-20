using Data.Entities.Interfaces;

namespace Data.Entities
{
    /// <summary>
    /// Автор
    /// </summary>
    public class Author : BaseEntity, IPerson
    {
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// Возраст
        /// </summary>
        public byte? Age { get; set; }
    }
}