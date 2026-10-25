using Dapper;
using Npgsql;
using System.Data;

namespace Server.Persistence
{
    public class GenericRepository<TEntity>(string connectionString) where TEntity : class
    {
        public async Task<TEntity?> Get(string query, object? param = null)
        {
            return await ExecuteOperations((connection) =>
                connection.QuerySingleOrDefaultAsync<TEntity>(query, param));
        }

        public async Task<long> Count(string query, object? param = null)
        {
            return await ExecuteOperations((connection) =>
                connection.QuerySingleOrDefaultAsync<long>(query, param));
        }

        protected async Task<T> ExecuteOperations<T>(Func<IDbConnection, Task<T>> queryAction)
        {
            await using var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            try
            {
                return await queryAction(connection);
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}