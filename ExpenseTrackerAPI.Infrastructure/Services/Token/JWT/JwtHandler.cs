using ExpenseTrackerAPI.Application.Abstraction.Token;
using ExpenseTrackerAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackerAPI.Infrastructure.Services.Token.JWT
{
    public class JwtHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpireIn;
        public JwtHandler(IConfiguration configuration)
        {
            _configuration = configuration;
            _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateAccessToken(User user)
        {
            _accessTokenExpireIn = DateTime.UtcNow.AddMinutes(_tokenOptions.AccessTokenExpiration);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.SecurityKey));

            var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwt = CreateJwtSecurityToken(user, _tokenOptions, signInCredentials);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new()
            {
                Token = token,
                ExpirationDate = _accessTokenExpireIn
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(User user, TokenOptions tokenOptions, SigningCredentials signingCredentials)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpireIn,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.Name, user.Email) }
                );

            return jwt;
        }
    }
}
