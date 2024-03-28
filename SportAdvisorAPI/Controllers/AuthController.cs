using Microsoft.AspNetCore.Mvc;
using SportAdvisorAPI.Contracts;
using SportAdvisorAPI.Models;

namespace SportAdvisorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        public AccountController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        // POST: api/Account/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Register(RegisterUserDto registerUser)
        {
            var errors = await _authManager.Register(registerUser);
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return Ok();
        }
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Login(LoginUserDTO loginUserDTO)
        {
            var authResponse = await _authManager.Login(loginUserDTO);
            if (authResponse is null)
            {
                return Unauthorized();
            }

            var IsEmailConfirmed = await _authManager.IsEmailConfirmed(loginUserDTO);
            if (!IsEmailConfirmed)
            {
                return Unauthorized("Email not confimed");
            }
            return Ok(authResponse);
        }

        [HttpGet]
        [Route("verify")]
        public async Task<ActionResult> VerifyEmail(Guid token)
        {
            var result = await _authManager.VerifyEmail(token);
            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok("Email has been confiremd");
            }
        }
    }
}