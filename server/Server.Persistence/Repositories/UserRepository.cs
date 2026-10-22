using Microsoft.Extensions.Options;
using Server.Application.Configurations;
using Server.Application.Interfaces.Repository;
using Server.Domain.Models;
using Server.Persistence.Context;

namespace Server.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context, IOptions<ConnectionStrings> connectionStrings)
        : GenericRepository<User>(connectionStrings.Value.DefaultConnection), IUserRepository
    {
        public async Task Signup(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
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
    }
}