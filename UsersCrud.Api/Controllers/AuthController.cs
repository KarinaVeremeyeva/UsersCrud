using Microsoft.AspNetCore.Mvc;
using UsersCrud.Auth.Services;

namespace UsersCrud.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        private const string Authorization = "Authorization";

        public AuthController(
            IAccountService accountService,
            ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("login")]
        public async Task<IActionResult> LoginAsync(string email, string password)
        {
            var result = await _accountService.LoginAsync(email, password);
            if (!result.Succeeded)
            {
                return BadRequest();
            }

            var token = _tokenService.CreateToken(email);
            Response.Headers.Add(Authorization, $"Bearer {token}");

            return Ok(token);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _accountService.LogoutAsync();

            return Ok();
        }
    }
}
