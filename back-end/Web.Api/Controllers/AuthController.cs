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
using Web.Api.Domain.IdentityAgg;
using Azure;
using Microsoft.AspNetCore.Authorization;

namespace Web.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly GoogleInfraService googleService;
        private readonly TokenService tokenService;
        private readonly SuperIngressoContext context;

        public AuthController(ILogger<AuthController> logger, 
            GoogleInfraService googleService, 
            TokenService tokenService,
            SuperIngressoContext context)
        {
            _logger = logger;
            this.googleService = googleService ?? throw new ArgumentNullException(nameof(googleService));
            this.tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }


        //TODO: Configurar o GoogleInfraService no IOC
        //TODO: Configurar a classe GoogleAuthConfig no IOC
        //TODO: Preencher o AppSettings de Development com as chaves de autenticação do google
        //TODO: Criar os relcionamentos entre User e Identity
        //TODO: Criar o mapeamento no EF Core
        //TODO: Adicionar o id do usuário vinculado ao identity na geração das claims



        [HttpPost("google/token")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleTokenAsync([FromBody] GoogleSignInModel model)
        {
            try
            {
                context.Set<LocalIdentity>().ExecuteDelete();
                context.Set<User>().ExecuteDelete();
                await context.SaveChangesAsync();
                //Obtém o Payload por meio do IdToken
                var payload = await googleService.ExtractPayloadFromIdToken(model);

                
                //Cria instancia do identity por meio do payload
                var identity = payload.ToIdentity();

                var user = UserFactory.CreateFromIdentity(identity);

                //Inclui e Salva o Usuário e Identidade
                context.Add(user);
                context.Add(identity);

                await context.SaveChangesAsync();

                var token = tokenService.CreateJwtToken(identity);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("login")]
        public IActionResult Login()
        {
            try
            {
                var props = new AuthenticationProperties { RedirectUri = Url.Action(nameof(GoogleLogin)) };
                return Challenge(props, GoogleDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpGet("signin-google")]
        public async Task<IActionResult> GoogleLogin()
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

                var exists = await context.Set<User>().AnyAsync(a => a.Login == userModel.Email);
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

                await context.Set<User>().AddAsync(user);
                await context.SaveChangesAsync();

                //return Ok(new TokenService(null).CreateToken(user));
                return BadRequest();
            }
            catch (Exception ex) when (ex.InnerException is SqlException)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

    }
}