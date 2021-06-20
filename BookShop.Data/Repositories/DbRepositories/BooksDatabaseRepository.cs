using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Data.Entities;
using Data.Factories.Interfaces;
using Data.Repositories.DbRepositories;
using Data.Repositories.Interfaces;

namespace Data.Repositories.DbRepositories
{
    public class BooksDatabaseRepository : BaseDatabaseRepository, IBooksRepository
    {
        public BooksDatabaseRepository(IDbTransaction transaction)
            : base(transaction)
        {
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IReadOnlyList<Book>> GetAllAsync()
        {
            var sql = "SELECT * FROM \"Books\" JOIN \"Authors\" ON \"Books\".\"AuthorId\" = \"Authors\".\"Id\"";
            var result = await Connection.QueryAsync<Book, Author, Book>(sql, (b, a) =>
                {
                    b.Authors.Add(a);
                    return b;
                },
                splitOn: "Id");
            return result.ToImmutableList();
        }

        public async Task<int> AddAsync(Book entity)
        {
            if (entity == null) return 0;

            var sql = "Insert into Books (AuthorId, Title, Desctiption) VALUES (@AuthorId,@Title,@Description)";
            return await Connection.ExecuteAsync(sql, entity, Transaction);
        }

        public Task<int> UpdateAsync(Book entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}