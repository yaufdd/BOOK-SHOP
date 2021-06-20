using System;

namespace Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBooksRepository BooksRepository { get; }

        void Commit();
    }
}