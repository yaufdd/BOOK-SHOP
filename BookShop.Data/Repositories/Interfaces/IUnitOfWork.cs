namespace Data.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IBooksRepository BooksRepository { get; }
    }
}