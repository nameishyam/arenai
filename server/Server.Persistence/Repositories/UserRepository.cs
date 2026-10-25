using Dapper;
using Microsoft.Extensions.Options;
using Npgsql;
using Server.Application.Configurations;
using Server.Application.Dto.Response;
using Server.Application.Interfaces.Repository;
using Server.Domain.Models;
using Server.Persistence.Context;

namespace Server.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context, IOptions<DbConfiguration> dbConfiguration)
        : GenericRepository<User>(dbConfiguration.Value.DefaultConnection), IUserRepository
    {
        public async Task Create(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task<GetMeResponse> GetMe(Guid id)
        {
            const string query = """
                SELECT "Id"
                    ,"FirstName"
                	,"LastName"
                	,"Email"
                FROM "Users"
                WHERE "Id" = @Id
                """;

            return await ExecuteOperations((connection) =>
                connection.QuerySingleAsync<GetMeResponse>(query, new { Id = id }));
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            const string query = """
                SELECT COUNT(*)
                FROM "Users"
                WHERE "Email" = @Email
                """;

            return await Count(query, new { Email = email }) > 0;
        }

        public async Task<bool> ExistsById(Guid id)
        {
            const string query = """
                SELECT COUNT(*)
                FROM "Users"
                WHERE "Id" = @Id
                """;

            return await Count(query, new { Id = id }) > 0;
        }
    }
}