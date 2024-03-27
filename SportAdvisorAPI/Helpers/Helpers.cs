using System.IdentityModel.Tokens.Jwt;

namespace SportAdvisorAPI.Helpers
{
    public static class HelperFunctions
    {
        public static string? GetJWTTokenClaim(string? token, string claimName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
            string? claimValue = securityToken.Claims.FirstOrDefault(c => c.Type == claimName)?.Value;
            return claimValue;
        }
    }
}