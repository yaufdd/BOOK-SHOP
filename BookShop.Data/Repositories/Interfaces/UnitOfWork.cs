namespace Data.Repositories.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBooksRepository BooksRepository { get; }
        
        public UnitOfWork(IBooksRepository booksRepository)
        {
            BooksRepository = booksRepository;
        } 
    }
}