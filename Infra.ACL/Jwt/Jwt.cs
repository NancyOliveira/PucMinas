using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infra.ACL.Jwt
{
    public class Jwt : IJwt
    {
        private readonly string secret;
        private readonly IConfiguration configuration;

        public Jwt(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.secret = configuration["Jwt:Secret"];
        }

        public string CreateToken(string login, DateTime expirationDate)
        {
            DateTime dataCriacao = DateTime.UtcNow;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, login)
                }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                NotBefore = dataCriacao
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
