using System.Collections;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<Author> Authors { get; set; }
    }
}