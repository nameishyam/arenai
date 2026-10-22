using Server.Domain.Models;

namespace Server.Application.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task Signup(User user);
        Task<bool> ExistsByEmail(string email);
    }
}