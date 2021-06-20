using System;
using System.Data;
using Data.Factories.Interfaces;
using Data.Repositories.DbRepositories;
using Data.Repositories.Interfaces;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IBooksRepository _booksRepository;

        public UnitOfWork(IDbConnectionFactory dbConnectionFactory,
            DatabaseConnectionTypes type = DatabaseConnectionTypes.Default
        )
        {
            _connection = dbConnectionFactory.CreateDbConnection(type);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IBooksRepository BooksRepository
            => _booksRepository ??= new BooksDatabaseRepository(_transaction);

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _booksRepository = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                ReleaseUnmanagedResources();
            }
        }

        private void ReleaseUnmanagedResources()
        {
            _transaction?.Dispose();
            _transaction = null;
            _connection?.Dispose();
            _connection = null;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}