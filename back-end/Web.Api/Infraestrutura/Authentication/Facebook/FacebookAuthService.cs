using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Web.Api.Infraestrutura.Authentication.Facebook
{
    public class FacebookAuthConfig
    {
        public string TokenValidationUrl { get; set; }
        public string UserInfoUrl { get; set; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }
    }
    /// <summary>
    /// Class Facebook Auth Service.
    /// Implements the <see cref="IFacebookAuthService" />
    /// </summary>
    /// <seealso cref="IFacebookAuthService" />
    public class FacebookAuthService : IFacebookAuthService
    {

        private readonly HttpClient _httpClient;
        private readonly FacebookAuthConfig _facebookAuthConfig;

        public FacebookAuthService(
            IHttpClientFactory httpClientFactory,
            IOptions<FacebookAuthConfig> facebookAuthConfig)
        {
            _httpClient = httpClientFactory.CreateClient("Facebook");
            _facebookAuthConfig = facebookAuthConfig.Value;
        }

        /// <summary>
        /// Validates Facebook Accesstoken
        /// </summary>
        /// <param name="accessToken">the accesstoken from facebook</param>
        /// <returns>Task&lt;BaseResponse&lt;FacebookTokenValidationResponse&gt;&gt;</returns>
        public async Task<FacebookTokenValidationResponse> ValidateFacebookToken(string accessToken)
        {
            try
            {
                string TokenValidationUrl = _facebookAuthConfig.TokenValidationUrl;
                var url = string.Format(TokenValidationUrl, accessToken, _facebookAuthConfig.AppId, _facebookAuthConfig.AppSecret);
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseAsString = await response.Content.ReadAsStringAsync();

                    var tokenValidationResponse = JsonConvert.DeserializeObject<FacebookTokenValidationResponse>(responseAsString);
                    return tokenValidationResponse;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;

        }

        /// <summary>
        /// Get Facebook User Information.
        /// </summary>
        /// <param name="accessToken">the access token from facebook</param>
        /// <returns>Task&lt;BaseResponse&lt;FacebookUserInfoResponse&gt;&gt;</returns>
        public async Task<FacebookUserInfoResponse> GetFacebookUserInformation(string accessToken)
        {
            try
            {
                string userInfoUrl = _facebookAuthConfig.UserInfoUrl;
                string url = string.Format(userInfoUrl, accessToken);

                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseAsString = await response.Content.ReadAsStringAsync();
                    var userInfoResponse = JsonConvert.DeserializeObject<FacebookUserInfoResponse>(responseAsString);
                    return userInfoResponse;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;

        }

    }
}
