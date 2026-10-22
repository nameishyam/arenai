using MediatR;
using Server.Application.Dto.Request;
using Server.Application.Dto.Response;

namespace Server.Application.Features.Commands.Users
{
    public record SignupCommand(SignupRequest SignupRequest)
        : IRequest<SignupResponse>;
}