using Microsoft.Extensions.Options;
using Web.Api.Models;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Web.Api.Infraestrutura
{
    public class GoogleInfraService
    {
        private readonly GoogleAuthConfig googleConfig;
        public GoogleInfraService(IOptions<GoogleAuthConfig> googleConfig)
        {
            this.googleConfig = googleConfig.Value;
        }
        public async Task<Payload> ExtractPayloadFromIdToken(GoogleSignInModel model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));

            return await ValidateAsync(model.IdToken, new ValidationSettings
            {
                Audience = new[] { googleConfig.ClientId }
            });
        }
    }
}
