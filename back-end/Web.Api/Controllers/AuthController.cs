using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Web.Api.Database;
using Web.Api.Extensions;
using Web.Api.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Web.Api.Domain.IdentityAgg;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Web.Api.Infraestrutura.Common;
using Newtonsoft.Json.Linq;
using Web.Api.Infraestrutura.Authentication;
using Web.Api.Infraestrutura.Authentication.Google;
using static Web.Api.Infraestrutura.Common.Answer;
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

        [HttpPost("google/token")]
        [AllowAnonymous]
        public async Task<Answer<TokenInfo>> GoogleTokenAsync([FromBody] GoogleSignInModel model)
        {
            try
            {
                //Obtém o Payload por meio do IdToken
                var payload = await googleService.ExtractPayloadFromIdToken(model);

                //Cria instancia do identity por meio do payload
                var identityCandidate = payload.ToIdentity();

                var identityCadastrado = context.Set<LocalIdentity>()
                    .Where(a => a.ProviderSubject == identityCandidate.ProviderSubject)
                    .Where(a => a.LoginProvider == identityCandidate.LoginProvider)
                    .FirstOrDefault()
                    ;

                if (identityCadastrado != null)
                {
                    identityCadastrado.Fill(identityCandidate);

                    if (!identityCadastrado.UserId.HasValue)
                    {
                        var newUser = UserFactory.CreateFromIdentity(identityCadastrado);
                        context.Add(identityCandidate);
                    }

                    context.Set<LocalIdentity>().Update(identityCadastrado);
                    await context.SaveChangesAsync();

                    var token = tokenService.CreateJwtToken(identityCadastrado);

                    return Answer.Ok(token);
                }
                else
                {
                    var user = UserFactory.CreateFromIdentity(identityCandidate);

                    //Inclui e Salva o Usuário e Identidade
                    context.Add(user);
                    context.Add(identityCandidate);

                    await context.SaveChangesAsync();

                    var token = tokenService.CreateJwtToken(identityCandidate);

                    return Answer.Ok(token);
                }
            }
            catch (Exception ex)
            {
                return Answer.Error<TokenInfo>(ex.Message);
            }
        }


        [HttpPost("facebook/token")]
        [AllowAnonymous]
        public async Task<Answer<TokenInfo>> FacebookTokenAsync([FromBody] FacebookSignInModel model)
        {
            return null;
            //    try
            //    {
            //        //Obtém o Payload por meio do IdToken
            //        var payload = await googleService.ExtractPayloadFromIdToken(model);

            //        //Cria instancia do identity por meio do payload
            //        var identityCandidate = payload.ToIdentity();

            //        var identityCadastrado = context.Set<LocalIdentity>()
            //            .Where(a => a.ProviderSubject == identityCandidate.ProviderSubject)
            //            .Where(a => a.LoginProvider == identityCandidate.LoginProvider)
            //            .FirstOrDefault()
            //            ;

            //        if (identityCadastrado != null)
            //        {
            //            identityCadastrado.Fill(identityCandidate);

            //            if (!identityCadastrado.UserId.HasValue)
            //            {
            //                var newUser = UserFactory.CreateFromIdentity(identityCadastrado);
            //                context.Add(identityCandidate);
            //            }

            //            context.Set<LocalIdentity>().Update(identityCadastrado);
            //            await context.SaveChangesAsync();

            //            var token = tokenService.CreateJwtToken(identityCadastrado);

            //            return Answer.Ok(token);
            //        }
            //        else
            //        {
            //            var user = UserFactory.CreateFromIdentity(identityCandidate);

            //            //Inclui e Salva o Usuário e Identidade
            //            context.Add(user);
            //            context.Add(identityCandidate);

            //            await context.SaveChangesAsync();

            //            var token = tokenService.CreateJwtToken(identityCandidate);

            //            return Answer.Ok(token);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        return Answer.Error<TokenInfo>(ex.Message);
            //    }
        }

        [HttpPost("local/signin")]
        [AllowAnonymous]
        public async Task<Answer<TokenInfo>> LocalSigninAsync([FromBody] UserModel userModel)
        {
            try
            {
                //Cria instancia do identity por meio do payload
                var identityCandidate = userModel.ToIdentity();

                var identityCadastrado = context.Set<LocalIdentity>()
                    .Where(a => a.ProviderSubject == identityCandidate.ProviderSubject)
                    .Where(a => a.LoginProvider == identityCandidate.LoginProvider)
                    .FirstOrDefault()
                    ;

                if (identityCadastrado != null)
                {
                    identityCadastrado.Fill(identityCandidate);

                    if (!identityCadastrado.UserId.HasValue)
                    {
                        var newUser = UserFactory.CreateFromIdentity(identityCadastrado);
                        context.Add(identityCandidate);
                    }

                    context.Set<LocalIdentity>().Update(identityCadastrado);
                    await context.SaveChangesAsync();

                    var token = tokenService.CreateJwtToken(identityCadastrado);

                    return Answer.Ok(token);
                }
                else
                {
                    var user = UserFactory.CreateFromIdentity(identityCandidate);

                    //Inclui e Salva o Usuário e Identidade
                    context.Add(user);
                    context.Add(identityCandidate);

                    await context.SaveChangesAsync();

                    var token = tokenService.CreateJwtToken(identityCandidate);

                    return Answer.Ok(token, "Usuário criado com sucesso, autenticação concluida.");
                }
            }
            catch (Exception ex)
            {
                return Answer.Error<TokenInfo>(ex.Message);
            }
        }

        [HttpPost("local/token")]
        [AllowAnonymous]
        public async Task<Answer<TokenInfo>> LocalTokenAsync([FromBody] LoginModel loginModel)
        {
            var identity = context.Set<LocalIdentity>()
                .Where(a => a.EmailOrLogin == loginModel.Login)
                .FirstOrDefault()
                ;

            if (identity == null) return º401<TokenInfo>("Usuário ou senha inválidos");

            if (identity.LoginProvider != LoginProvider.Local) return º401<TokenInfo>("Usuário ou senha inválidos");

            var samePws = identity.Password == loginModel.Senha?.ToSha256();
            if (!samePws) return º401<TokenInfo>("Usuário ou senha inválidos");

            var token = tokenService.CreateJwtToken(identity);

            return Answer.Ok(token, "Usuário autenticado com sucesso!");
        }

    }
}