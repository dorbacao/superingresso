namespace Web.Api.Domain.IdentityAgg
{
    public static class UserFactory
    {
        public static User CreateFromIdentity(LocalIdentity identity)
        {
            var user = new User(identity);
            identity.UserId = user.Id;
            identity.User = user;

            user.Nome = identity.GivenName;
            user.SobreNome = identity.SurName;
            user.Ativo = true;
            user.DataInclusao = DateTime.Now;
            user.Login = identity.EmailOrLogin;
            user.Email = identity.EmailOrLogin;
            
            return user;
        }
    }
    public class User
    {
        public User()
        {
            Ativo = true;
            Identities = new List<LocalIdentity>();
            Id  = Guid.NewGuid();
        }

        internal User(LocalIdentity identity):this()
        {
            if (identity is null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            Identities.Add(identity);
            
        }
        public Guid Id { get; set; }

        public IList<LocalIdentity> Identities { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Login { get; set; }
        public string? Senha { get; set; }
        public bool? Ativo { get; set; }
        public string CodigoPostal { get; set; }
        public string Estado { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime? DataInclusao { get; set; }


    }
}
