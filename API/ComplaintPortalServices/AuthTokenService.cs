using ComplaintPortalEntities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintPortalServices
{  
    public class AuthTokenService
    {
        private readonly ApplicationSettings _applicationSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public AuthTokenService(ApplicationSettings applicationSettings, TokenValidationParameters tokenValidationParameters) 
        {
            _applicationSettings = applicationSettings;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public string GenerateAccessToken(AuthModel authUser)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, authUser.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, authUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //new Claim(JwtRegisteredClaimNames.Iat, _dateTimeProvider.DateTimeOffsetNow.ToString()),
                new Claim(nameof(authUser.UserId), authUser.UserId.ToString()),
                new Claim(nameof(authUser.FullName), authUser.FullName),
            };

            var token = CreateToken(claims);
            return token;
        }

        public string CreateToken(IList<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_applicationSettings.JwtSettings.SecretKey));

            var siginingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
               issuer: _applicationSettings.JwtSettings.Issuer,
               audience: _applicationSettings.JwtSettings.Audience,
               expires: DateTime.Now.AddMinutes(_applicationSettings.JwtSettings.ExpiryMinutes),
               claims: claims,
               signingCredentials: siginingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshtoken = Convert.ToBase64String(tokenBytes);

            return refreshtoken;
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            //Validation 1 - Validation JWT token format
            _tokenValidationParameters.ValidateLifetime = false;
            var tokenInVerification = jwtTokenHandler.ValidateToken(token,
                                                                    _tokenValidationParameters,
                                                                    out var validatedToken);
            _tokenValidationParameters.ValidateLifetime = true;

            //Validation 2 - Validate encryption alg
            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                if (result == false)
                {
                    throw new KeyNotFoundException("Invalid Token.");
                }
            }

            // Validation 3 - validate expiry date
            var expiry = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)?.Value;
            if (!string.IsNullOrEmpty(expiry))
            {
                DateTimeOffset expiryDate = UnixTimeStampToDateTime(long.Parse(expiry));

                if (expiryDate > DateTime.Now)
                {
                    throw new KeyNotFoundException("Token has not yet expired.");
                }
            }

            return tokenInVerification;
        }

        private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToLocalTime();

            return dateTimeVal;
        }
    }
}
