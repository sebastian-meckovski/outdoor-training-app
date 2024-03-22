
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using OutdoorTraining.Models;
using PullupBars.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OutdoorTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PullupBarsAPIDbContext _dbContext;
        // public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public UserController(
          PullupBarsAPIDbContext dbContext,
          IConfiguration configuration,
          UserManager<AppUser> userManager,
          SignInManager<AppUser> signInManager
          )
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            var user = new AppUser
            {
                Email = request.Email,
                UserName = request.Email,
                VerificationToken = CreateRandomToken()
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return BadRequest("Invalid email or password.");
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (result.Succeeded)
            {
                // If sign-in succeeded, you might generate a token or return some user information
                return Ok(new { Message = "Login successful", User = user });
            }
            else
            {
                // If sign-in failed, return an error message
                return BadRequest("Invalid email or password.");
            }
        }

        [HttpGet("verify")]
        public async Task<IActionResult> Verify(string token, string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null || user.VerificationToken != token)
            {
                return BadRequest("Invalid token.");
            }

            user.EmailConfirmed = true;
            user.VerificationToken = null;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok("user " + user.Email + " confirmed");
            }
            else
            {
                return BadRequest("Invalid token");
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            // TODO
            // Email logic needs to go in this controller
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("ResetPassword", "Auth", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);

            return Ok("this is the link: " + callbackUrl);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            // _userManager.ResetPasswordAsync(user, request.)
            // string passwordHash
            //                = BCrypt.Net.BCrypt.HashPassword(request.Password);
            // user.PasswordHash = passwordHash;
            // user.PasswordResetToken = null;
            // user.ResetTokenExpires = null;

            // var result = await _userManager.UpdateAsync(user, request.Password);

            return Ok("Reset password is work in progress...");
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        [HttpPost]
        [Route("SignInWithGoogle")]
        public async Task<IActionResult> SignInWithGoogle(Credential credential)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "875031025071-tbnars2a8b5qv3ihcg2gb3tf3oclmept.apps.googleusercontent.com" }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(credential.credential, settings);
            var user = payload.Name;
            var email = payload.Email;
            return Ok(user + email);
        }

        [NonAction]
        private string CreateToken(AppUser user)
        {
            List<Claim> claims = new()
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                 _configuration.GetSection("AppSettings:Token").Value!));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha384);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
