using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Application.Dto.Request;
using Server.Application.Exceptions;
using Server.Application.Features.Commands.Auth;

namespace Server.Api.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
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

                return CreatedAtAction(
                    nameof(GetMe),
                    new { id = response.Id },
                    response);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id:guid}/me")]
        public async Task<IActionResult> GetMe([FromRoute] Guid id)
        {
            try
            {
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
