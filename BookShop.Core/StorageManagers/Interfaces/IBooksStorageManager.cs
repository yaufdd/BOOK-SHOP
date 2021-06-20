using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace Core.StorageManagers.Interfaces
{
    public interface IBooksStorageManager
    {
        Task<IReadOnlyList<Book>> GetAll();
    }
}