using Data.Entities.Interfaces;

namespace Data.Entities
{
    public class Author : BaseEntity, IPerson
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public byte? Age { get; set; }
    }
}