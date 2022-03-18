using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TokenProvider.Model;

namespace TokenProvider.Logic
{
    public class TokenLogic : ITokenLogic
    {
        private readonly ICryptoLogic _cryptoLogic;
        private readonly IConfiguration _configuration;

        public TokenLogic(ICryptoLogic cryptoLogic, IConfiguration configuration)
        {
            _cryptoLogic = cryptoLogic;
            _configuration = configuration;
        }
        
        public Token CreateToken()
        {
            var privateKey = _cryptoLogic.GetPrivateKey();
            
            var rsa = RSA.Create();
            rsa.ImportRSAPrivateKey(
                source: Convert.FromBase64String(privateKey),
                bytesRead: out int _);

            var signingCredentials = new SigningCredentials(
                key: new RsaSecurityKey(rsa),
                algorithm: SecurityAlgorithms.RsaSha256
            );

            var jwtDate = DateTime.Now;
            
            var jwt = new JwtSecurityToken(
                audience: _configuration["Jwt:Audience"],
                issuer: _configuration["Jwt:Issuer"],
                claims: new[] {new Claim(ClaimTypes.NameIdentifier, "some-username")},
                notBefore: jwtDate,
                expires: jwtDate.AddMinutes(60),
                signingCredentials: signingCredentials
            );
            
            var token = new Token
            {
                Jwt = new JwtSecurityTokenHandler().WriteToken(jwt),
                ExpiresAt = jwtDate
            };

            return token;
        }
    }
}