using System.Collections.Generic;
using System.Threading.Tasks;
using Core.StorageManagers.Interfaces;
using Data.Entities;
using Data.Repositories.Interfaces;

namespace Core.StorageManagers
{
    public class BooksStorageManager : IBooksStorageManager
    {
        private readonly IUnitOfWork _uow;

        public BooksStorageManager(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyList<Book>> GetAll()
            => await _uow.BooksRepository.GetAllAsync();
    }
}