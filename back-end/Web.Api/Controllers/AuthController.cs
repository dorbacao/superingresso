using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Web.Api.Database;
using Web.Api.Domain;
using Web.Api.Extensions;
using Web.Api.Infraestrutura;
using Web.Api.Models;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger, SuperIngressoContext context)
        {
            _logger = logger;
            Context = context;
        }

        public SuperIngressoContext Context { get; }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserModel userModel)
        {
            try
            {

                var exists = await Context.Set<User>().AnyAsync(a => a.Login == userModel.Email);
                if (exists)
                {
                    return BadRequest("Este usuário já existe");
                }

                var user = new User();

                user.Id = (userModel.Id == null || Guid.Empty.Equals(userModel.Id)) ? Guid.NewGuid() : Guid.Parse(userModel.Id);
                user.Nome = userModel.Nome;
                user.Login = userModel.Email;
                user.Email = userModel.Email;

                if (userModel.Senha != userModel.ConfirmarSenha)
                {
                    return BadRequest("Os campos 'Senha' e 'Confirmar Senha' não podem ser diferentes!");
                }

                user.Senha = userModel.Senha.ToSha256();

                await Context.Set<User>().AddAsync(user);
                await Context.SaveChangesAsync();

                return Ok(new TokenService().CreateToken(user));
            }
            catch (Exception ex) when (ex.InnerException is SqlException)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

    }
}