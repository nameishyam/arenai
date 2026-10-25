using Server.Application.Dto.Response;
using Server.Domain.Models;

namespace Server.Application.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task Create(User user);
        Task<bool> ExistsById(Guid id);
        Task<bool> ExistsByEmail(string email);
        Task<GetMeResponse> GetMe(Guid id);
    }
}