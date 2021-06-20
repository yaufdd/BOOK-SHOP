using System.Data;
using Data.Factories.Interfaces;

namespace Data.Repositories.Interfaces
{
    public abstract class BaseRepository
    {
        protected IDbConnection Connection;

        protected BaseRepository(IDbConnectionFactory dbConnectionFactory)
        {
            Connection = dbConnectionFactory.CreateDbConnection(DatabaseConnectionTypes.Default);
        }
    }
}