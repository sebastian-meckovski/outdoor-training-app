using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PullupBars.Data;
using System.Security.Cryptography;
using OutdoorTraining.Models;

namespace OutdoorTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PullupBarsAPIDbContext dbContext;

        public UserController(PullupBarsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (dbContext.Users.Any(u => u.Email == request.Email))
            {
                return BadRequest("User already exists.");
            }

            string passwordHash
                  = BCrypt.Net.BCrypt.HashPassword(request.Password);


            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                VerificationToken = CreateRandomToken()
            };

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return Ok("User successfully created!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Password is incorrect.");
            }

            if (user.VerifiedAt == null)
            {
                return BadRequest("Not verified!");
            }

            return Ok($"Welcome back, {user.Email}! :)");
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.VerificationToken == token);
            if (user == null)
            {
                return BadRequest("Invalid token.");
            }

            user.VerifiedAt = DateTime.Now;
            await dbContext.SaveChangesAsync();

            return Ok("User verified! :)");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddDays(1);
            await dbContext.SaveChangesAsync();

            return Ok("You may now reset your password.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResettPassword(ResetPasswordRequest request)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);
            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return BadRequest("Invalid Token.");
            }

            string passwordHash
                           = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.PasswordHash = passwordHash;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await dbContext.SaveChangesAsync();

            return Ok("Password successfully reset.");
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
