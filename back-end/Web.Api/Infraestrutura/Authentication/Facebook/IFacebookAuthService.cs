using Web.Api.Infraestrutura.Common;

namespace Web.Api.Infraestrutura.Authentication.Facebook
{
    public interface IFacebookAuthService
    {
        Task<FacebookTokenValidationResponse> ValidateFacebookToken(string accessToken);
        Task<FacebookUserInfoResponse> GetFacebookUserInformation(string accessToken);
    }
}
