using MediatR;
using Server.Application.Dto.Response;
using Server.Application.Exceptions;
using Server.Application.Features.Commands.Auth;
using Server.Application.Interfaces.Repository;
using Server.Application.Interfaces.Service;
using Server.Domain.Models;

namespace Server.Application.Features.Handlers.Auth
{
    public class SignupHandler(IUserRepository userRepository, IJwtService jwtService, IEmailService emailService)
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

            await userRepository.Create(user);

            await emailService.SendEmailAsync(user.Email, "Welcome to ArenAI", "Glad to have you here");

            return new SignupResponse
            {
                Id = user.Id,
                AccessToken = jwtService.GenerateToken(user.Id, user.Email)
            };
        }
    }
}