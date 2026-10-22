using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Dto.Request;
using Server.Application.Exceptions;
using Server.Application.Features.Commands.Users;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        private const string CookieName = "access_token";

        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequest signupRequest)
        {
            try
            {
                var response = await mediator.Send(new SignupCommand(signupRequest));

                SetAuthCookie(response.AccessToken);

                return StatusCode(201, new
                {
                    response.Id,
                    response.AccessToken
                });
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            //catch (Exception ex)
            //{
            //    return StatusCode(500, ex.Message);
            //}
        }

        private void SetAuthCookie(string token)
        {
            Response.Cookies.Append(
                CookieName,
                token,
                new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(7)
                });
        }
    }
}