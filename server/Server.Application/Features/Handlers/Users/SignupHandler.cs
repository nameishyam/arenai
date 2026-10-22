using MediatR;
using Server.Application.Dto.Response;
using Server.Application.Exceptions;
using Server.Application.Features.Commands.Users;
using Server.Application.Interfaces.Repository;
using Server.Application.Interfaces.Service;
using Server.Domain.Models;

namespace Server.Application.Features.Handlers.Users
{
    public class SignupHandler(
        IUserRepository userRepository,
        IJwtService jwtService)
        : IRequestHandler<SignupCommand, SignupResponse>
    {
        public async Task<SignupResponse> Handle(SignupCommand request, CancellationToken cancellationToken)
        {
            if (await userRepository.ExistsByEmail(request.SignupRequest.Email))
            {
                throw new ConflictException(request.SignupRequest.Email);
            }

            var user = new User
            {
                FirstName = request.SignupRequest.FirstName,
                LastName = request.SignupRequest.LastName,
                Email = request.SignupRequest.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.SignupRequest.Password)
            };

            await userRepository.Signup(user);

            return new SignupResponse
            {
                Id = user.Id,
                AccessToken = jwtService.GenerateToken(user.Id, request.SignupRequest.Email)
            };
        }
    }
}
