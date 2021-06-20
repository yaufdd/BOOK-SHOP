using System.Collections;
using System.Collections.Generic;

namespace Data.Entities
{
    /// <summary>
    /// Книга
    /// </summary>
    public class Book : BaseEntity
    {
        /// <summary>
        /// Название книги
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Описание книги
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Список авторов книги
        /// </summary>
        public IList<Author> Authors { get; set; }

        public Book()
        {
            Authors = new List<Author>();
        }
    }
}