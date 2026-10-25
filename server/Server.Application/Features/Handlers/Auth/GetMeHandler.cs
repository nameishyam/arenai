using MediatR;
using Server.Application.Dto.Response;
using Server.Application.Exceptions;
using Server.Application.Features.Queries.Auth;
using Server.Application.Interfaces.Repository;

namespace Server.Application.Features.Handlers.Auth
{
    public class GetMeHandler(IUserRepository userRepository) : IRequestHandler<GetMeQuery, GetMeResponse>
    {
        public async Task<GetMeResponse> Handle(GetMeQuery getMeQuery, CancellationToken cancellationToken)
        {
            if (!await userRepository.ExistsById(getMeQuery.Id))
            {
                throw new NotFoundException();
            }

            return await userRepository.GetMe(getMeQuery.Id);
        }
    }
}