using MediatR;
using Server.Application.Dto.Response;

namespace Server.Application.Features.Queries.Auth
{
    public record GetMeQuery(Guid Id)
        : IRequest<GetMeResponse>;
}