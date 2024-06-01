using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Api.Database;
using Web.Api.Domain;
using Web.Api.Extensions;
using Web.Api.Models;

namespace Web.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, SuperIngressoContext context)
        {
            _logger = logger;
            Context = context;
        }

        public SuperIngressoContext Context { get; }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserModel userModel)
        {
            var user = new User();

            user.Id = Guid.Empty.Equals(userModel.Id) ? Guid.NewGuid() : userModel.Id;
            user.Nome = userModel.Nome;
            user.Login = userModel.Login;
            user.Senha = userModel.Senha.ToSha256();

            await Context.Set<User>().AddAsync(user);
            await Context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel userLogin)
        {
            // Validação do usuário e senha (a implementar)
            var userIsValid = true; // Substituir pela lógica de validação

            if (!userIsValid)
            {
                return Unauthorized();
            }

            var senha = userLogin.Login.ToSha256();
            var user = Context.Set<User>()
                .Where(a => a.Login == userLogin.Login)
                .FirstOrDefault(a => a.Senha == senha)
                ;

            if (user == null)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("8f14e45fceea167a5a36dedd4bea2543f391a2b3e7e9d8fd28e4e7c13b3f1e2c\r\n");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userLogin.Login)
                }),
                Expires = DateTime.UtcNow.AddHours(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { Token = tokenString });
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return Context.Set<User>().ToList();
        }
    }
}