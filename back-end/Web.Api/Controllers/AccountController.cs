using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Web.Api.Database;
using Web.Api.Extensions;
using Web.Api.Infraestrutura;
using Web.Api.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Web.Api.Domain.IdentityAgg;

namespace Web.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, SuperIngressoContext context)
        {
            _logger = logger;
            Context = context;
        }

        public SuperIngressoContext Context { get; }

        [HttpGet("login")]
        public IActionResult Login()
        {
            try
            {
                var props = new AuthenticationProperties { 
                    RedirectUri = Url.Action("Authorize"),
                };

               // return Challenge(props, CookieAuthenticationDefaults.AuthenticationScheme);
                return Challenge(props, GoogleDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("signin-google")]
        public async Task<IActionResult> Authorize()
        {
            var response = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (response.Principal == null) return BadRequest();

            var name = response.Principal.FindFirstValue(ClaimTypes.Name);
            var givenName = response.Principal.FindFirstValue(ClaimTypes.GivenName);
            var email = response.Principal.FindFirstValue(ClaimTypes.Email);
            //Do something with the claims
            // var user = await UserService.FindOrCreate(new { name, givenName, email});

            return Ok();
        }

        [HttpGet("authorize")]
        public async Task<IActionResult> Authorize2()
        {
            var response = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (response.Principal == null) return BadRequest();

            var name = response.Principal.FindFirstValue(ClaimTypes.Name);
            var givenName = response.Principal.FindFirstValue(ClaimTypes.GivenName);
            var email = response.Principal.FindFirstValue(ClaimTypes.Email);
            //Do something with the claims
            // var user = await UserService.FindOrCreate(new { name, givenName, email});

            return Ok();
        }

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

                //return Ok(new TokenService().CreateToken(user));
                return BadRequest();
            }
            catch (Exception ex) when (ex.InnerException is SqlException)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

    }
}