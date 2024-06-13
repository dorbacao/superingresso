using IdentityModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Api.Domain.IdentityAgg;

namespace Web.Api.Infraestrutura
{
    public struct TokenInfo
    {
        public string Token { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public Guid Id { get; set; }
        public LocalIdentity Identity { get; set; }
    }

    //TODO: Configutar TokenService no IOC
    public class TokenService
    {
        private readonly JwtConfig jwtConfig;

        public TokenService(IOptions<JwtConfig> jwtConfig)
        {
            if (jwtConfig is null)
            {
                throw new ArgumentNullException(nameof(jwtConfig));
            }

            this.jwtConfig = jwtConfig.Value;
        }
        /// <summary>
        /// Creates JWT Token
        /// </summary>
        /// <param name="identity">the user</param>
        /// <returns>System.String</returns>
        public TokenInfo CreateJwtToken(LocalIdentity identity)
        {

            var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            var userClaims = BuildUserClaims(identity);

            var signKey = new SymmetricSecurityKey(key);

            //TODO: Copiar as configurações do JWT e colocar no appsettings.json

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtConfig.ValidIssuer,
                notBefore: DateTime.UtcNow,
                audience: jwtConfig.ValidAudience,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(jwtConfig.DurationInMinutes)),
                claims: userClaims,
                signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var tokenInfo = new TokenInfo()
            {
                Nome = identity.GivenName,
                Login = identity.SurName,//TODO:Verificar a possibilidade de remover este campo login
                Id = identity.Id,
                Token = token,
                Identity = identity
            };

            return tokenInfo;
        }

        /// <summary>
        /// Builds the UserClaims
        /// </summary>
        /// <param name="identity">the User</param>
        /// <returns>List&lt;System.Security.Claims&gt;</returns>
        private List<Claim> BuildUserClaims(LocalIdentity identity)
        {
            var userClaims = new List<Claim>()
            {
                new Claim(JwtClaimTypes.Id, identity.Id.ToString()),
                new Claim(JwtClaimTypes.Email, identity.EmailOrLogin),
                //new Claim(JwtClaimTypes.EmailVerified, identity.EmailOrLogin),
                new Claim(JwtClaimTypes.GivenName, identity.GivenName),
                new Claim(JwtClaimTypes.FamilyName, identity.SurName),
                new Claim(JwtClaimTypes.Picture, identity.PictureUrl),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            return userClaims;
        }

    }
}
