using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using SportAdvisorAPI.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SportAdvisorAPI.Models;

namespace SportAdvisorAPI.Repository
{
    public class AuthManagerRepository : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;


        public AuthManagerRepository(IMapper mapper, UserManager<User> userManager, IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponseDTO?> Login(LoginUserDTO loginUserDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDTO.Email);
            if (user == null)
            {
                return null;
            }
            var validPassword = await _userManager.CheckPasswordAsync(user, loginUserDTO.Password);
            if (validPassword)
            {
                var token = await GenerateToken(user);
                var responseObject = new AuthResponseDTO
                {
                    Token = token,
                    UserId = user.Id
                };
                return responseObject;
            }

            return null;
        }

        public async Task<IEnumerable<IdentityError>> Register(RegisterUserDto registerUserDtouserDto)
        {
            var user = _mapper.Map<User>(registerUserDtouserDto);
            user.UserName = registerUserDtouserDto.Email;

            var result = await _userManager.CreateAsync(user, registerUserDtouserDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            return result.Errors;
        }
        public async Task<string> GenerateToken(User user)
        {
            var jwtKey = _configuration["JwtSettings:Key"] ?? throw new InvalidOperationException("JWT Key is not configured properly.");
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            var userClaims = await _userManager.GetClaimsAsync(user);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email is null ? "" : user.Email),
            }.Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}