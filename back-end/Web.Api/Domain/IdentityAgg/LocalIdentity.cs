namespace Web.Api.Domain.IdentityAgg
{
    public class LocalIdentity
    {
        /// <summary>
        /// Identificador único local do mega Ingresso
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// ProviderSubject é o identificador único do usuário no provedor de autenticação.
        /// </summary>
        public string? ProviderSubject { get; set; }

        public string? EmailOrLogin { get; set; }
        public string? Password { get; set; }
        public string? GivenName { get; set; }
        public string? SurName { get; set; }
        public string? PictureUrl { get; set; }
        public LoginProvider? LoginProvider { get; set; }
        public Guid? UserId { get; set; }
        public User User { get; set; }
    }
}
