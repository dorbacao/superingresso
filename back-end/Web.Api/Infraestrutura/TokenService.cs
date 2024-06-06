using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Api.Domain;

namespace Web.Api.Infraestrutura
{
    public struct TokenInfo
    {
        public string Token { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public Guid Id { get; set; }
    }
    public class TokenService
    {
        
        public TokenInfo CreateToken(User user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("8f14e45fceea167a5a36dedd4bea2543f391a2b3e7e9d8fd28e4e7c13b3f1e2c\r\n");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Login)
                }),
                Expires = DateTime.UtcNow.AddHours(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var tokenInfo = new TokenInfo()
            {
                Nome = user.Nome,
                Login = user.Login,
                Id = user.Id,
                Token = tokenString
            };

            return tokenInfo;
        }
    }
}
