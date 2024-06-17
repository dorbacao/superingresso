using Web.Api.Domain.IdentityAgg;
using Web.Api.Extensions;

namespace Web.Api.Models
{

    public static class UserModelExtensions
    {
        public static LocalIdentity ToIdentity(this UserModel model)
        {
            var identity = new LocalIdentity();
            
            identity.Id = Guid.NewGuid();
            identity.EmailOrLogin = model.Email;
            identity.LoginProvider = LoginProvider.Local;
            identity.GivenName = model.Nome;
            identity.SurName = model.SobreNome;
            identity.Password= model.Senha?.ToSha256();
            identity.ProviderSubject = identity.Id.ToString();

            return identity;
        }
    }
    public class UserModel
    {
        public string? Id { get; set; }
        public string? Nome { get; set; }
        
        //TODO: Remover login e considerar apenas email
        public string? Login { get; set; }
        public string? Senha { get; set; }
        public string? ConfirmarSenha { get; set; }
        public string? SobreNome { get; set; }
        public string? Email { get; set; }
        public string? Telefone { get; set; }
        public string? Endereco { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? CodigoPostal { get; set; }
    }

}
