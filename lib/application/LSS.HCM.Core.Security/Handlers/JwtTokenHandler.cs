using LSS.HCM.Core.DataObjects.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace LSS.HCM.Core.Security.Handlers
{
    public class JwtTokenHandler : IJwtTokenHandler 
    {
        private readonly AppSettings _appSettings;
        public JwtTokenHandler(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }
        public string GenerateJwtSecurityToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Api.JsonWebTokens.Secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var result = tokenHandler.WriteToken(token);
            return result;
        }

        public string PrepareTokenFromAccessToekn(string accessToken)
        {
            string token = string.Empty;
            if (!string.IsNullOrEmpty(accessToken))
            {
                var tokenArray = accessToken.Split(" ");
                token = tokenArray[1];
            }
            return token;
        }

        public (bool, string) VerifyJwtSecurityToken(string token)
        {
            string claimsIdentity = string.Empty;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Api.JsonWebTokens.Secret));
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signinKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                };
                var claims = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken).Claims.ToList();
                claimsIdentity = claims.FirstOrDefault(c => c.Type == "transaction_id").Value;
                return (true, claimsIdentity);
            }
            catch
            {
                return (false, claimsIdentity);
            }

        }
    }
}
