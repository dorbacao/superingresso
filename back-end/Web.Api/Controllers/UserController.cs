using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Web.Api.Database;
using Web.Api.Domain.IdentityAgg;
using Web.Api.Extensions;
using Web.Api.Infraestrutura.Common;
using Web.Api.Models;

namespace Web.Api.Controllers
{

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, SuperIngressoContext context)
        {
            var x = this.Url;
            _logger = logger;
            Context = context;
        }

        public SuperIngressoContext Context { get; }


        [HttpGet]
        public async Task<AnswerPaginate<List<User>>> GetUsersAsync([FromQuery] PaginateCommand command)
        {
            var users = await Context.Set<User>()
                    .OrderBy(command)
                    .Paginate(command)
                    //.Select(a => new { a.Id, a.Telefone, a.Nome, a.SobreNome, a.Email })
                    .ToListAsync<User>();
                    ;
            
            var count = await Context.Set<User>().LongCountAsync();

            return Answer.Ok(users, count);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserModel userModel)
        {
            var user = new User();

            user.Id = Guid.Empty.Equals(userModel.Id) ? Guid.NewGuid() : Guid.Parse(userModel.Id);
            user.Nome = userModel.Nome;
            user.Login = userModel.Login;
            user.Senha = userModel.Senha.ToSha256();

            await Context.Set<User>().AddAsync(user);
            await Context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut()]
        public async Task<IActionResult> Update2([FromBody] UserModel userModel)
        {
            return Ok();
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UserModel userModel)
        {
            var user = await Context.Set<User>().FirstOrDefaultAsync(user => user.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            user.Nome = userModel.Nome;
            user.SobreNome = userModel.SobreNome;
            user.Email = userModel.Email;
            user.Endereco = userModel.Endereco;
            user.Telefone = userModel.Telefone;
            user.Cidade = userModel.Cidade;
            user.Estado = userModel.Estado;
            user.CodigoPostal = userModel.CodigoPostal;

            Context.Set<User>().Attach(user);
            await Context.SaveChangesAsync();

            return Ok("Usuário alterado com sucesso");
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

            var senha = userLogin.Senha.ToSha256();
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

            return Ok(new
            {
                Token = tokenString,
                Login = user.Login,
                Nome = user.Nome,
                Id = user.Id
            });
        }

        [HttpGet("{id:guid}")]
        public async Task<User> Get([FromRoute] Guid id)
        {
            var user = await Context
                .Set<User>()
                .FirstOrDefaultAsync(a => a.Id == id)
                ;

            return user;
        }

        [HttpPatch("{id}/password")]
        public async Task<IActionResult> ChangePasswordAsync([FromRoute] Guid id, [FromBody] PasswordModel passWordModel)
        {
            var user = await Context
                .Set<User>()
                .FirstOrDefaultAsync(a => a.Id == id)
                ;

            if (user == null)
            {
                return NotFound();
            }

            if (passWordModel.Senha != passWordModel.ConfirmaSenha)
            {
                return BadRequest("Os campos 'Senha' e 'Confirmar Senha' não podem ser diferentes");
            }

            user.Senha = passWordModel.Senha.ToSha256();

            Context.Set<User>().Update(user);
            await Context.SaveChangesAsync();

            return Ok(user);
        }
    }
}