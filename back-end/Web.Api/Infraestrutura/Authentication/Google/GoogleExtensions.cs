using Web.Api.Domain.IdentityAgg;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Web.Api.Infraestrutura.Authentication.Google
{
    public static class GoogleExtensions
    {
        public static LocalIdentity ToIdentity(this Payload payload)
        {
            return new LocalIdentity()
            {
                Id = Guid.NewGuid(),
                GivenName = payload.GivenName,
                SurName = payload.FamilyName,
                EmailOrLogin = payload.Email,
                LoginProvider = LoginProvider.Google,
                PictureUrl = payload.Picture,
                ProviderSubject = payload.Subject,
            };
        }
    }
}
