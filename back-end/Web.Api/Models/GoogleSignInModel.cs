using System.ComponentModel.DataAnnotations;

namespace Web.Api.Models
{
    public class GoogleSignInModel
    {
        [Required]
        public string IdToken { get; set; }
    }

    public class FacebookSignInModel
    {
        /// <summary>
        /// This token is generated from the client side. i.e. react, angular, flutter etc.
        /// </summary>
        [Required]
        public string AccessToken { get; set; }
    }

}
