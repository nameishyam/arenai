using MediatR;
using Server.Application.Dto.Request;
using Server.Application.Dto.Response;

namespace Server.Application.Features.Commands.Auth
{
    public record SignupCommand(SignupRequest SignupRequest)
        : IRequest<SignupResponse>;
}
