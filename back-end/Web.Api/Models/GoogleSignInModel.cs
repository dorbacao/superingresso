using System.ComponentModel.DataAnnotations;

namespace Web.Api.Models
{
    public class GoogleSignInModel
    {
        [Required]
        public string IdToken { get; set; }
    }

}
