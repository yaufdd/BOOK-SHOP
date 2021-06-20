using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Data.Entities;
using Data.Factories.Interfaces;
using Data.Repositories.Interfaces;
using Npgsql;

namespace Data.Repositories
{
    public class BooksRepository : BaseRepository, IBooksRepository
    {
        public BooksRepository(IDbConnectionFactory dbConnectionFactory)
            : base(dbConnectionFactory)
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

        public Task<Book> AddAsync(Book entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Book> UpdateAsync(Book entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<Book> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}