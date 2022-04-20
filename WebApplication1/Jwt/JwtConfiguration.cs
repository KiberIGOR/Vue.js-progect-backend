using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Jwt
{
    public class JwtConfiguration
    {
        private readonly string _key = "asdasdasdasdasdasdasdas";

        public readonly string ISSUER = "ISSUER";

        public readonly string AUDIENCE = "LDAP-CLIENT";

        public readonly int LIFETIME_MS = 100000000;

        public JwtConfiguration()
        {
        }

        public string GenerateToken(int userId, string userLogin)
        {
            if (userId == 0 ||
                string.IsNullOrEmpty(userLogin))
            {
                return null;
            }

            var claims = new List<Claim> {
                new Claim("id", userId.ToString()),
                new Claim("login", userLogin),
                new Claim("lifetimems", LIFETIME_MS.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token", "login", ClaimTypes.Role);

            var utcNow = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                    issuer: ISSUER,
                    audience: AUDIENCE,
                    claims: claimsIdentity.Claims,
                    expires: utcNow.Add(TimeSpan.FromMilliseconds(LIFETIME_MS)),
                    signingCredentials: new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public TokenValidationParameters TokenValidationParameters(bool doNotCheckLifetime = true)
        {
            var validationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = false,
                // Specify the key used to sign the token:
                IssuerSigningKey = SymmetricSecurityKey,
                RequireExpirationTime = false,
                // Ensure the token audience matches our audience value (default true):
                ValidateAudience = false,
                ValidAudience = AUDIENCE,
                // Ensure the token was issued by a trusted authorization server (default true):
                ValidateIssuer = false,
                ValidIssuer = ISSUER
            };

            if (doNotCheckLifetime)
            {
                validationParams.ValidateLifetime = false;
            }
            else
            {
                validationParams.ValidateLifetime = true;
                validationParams.LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
                {
                    if (!expires.HasValue)
                    {
                        return false;
                    }

                    if (expires.Value <= DateTime.UtcNow)
                    {
                        return false;
                    }

                    return true;
                };
            }

            return validationParams;
        }

        public SymmetricSecurityKey SymmetricSecurityKey
            => new SymmetricSecurityKey(Encoding.UTF32.GetBytes(_key));
    }
}
