namespace Web.Api.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public string CodigoPostal{ get; set; }
        public string Estado { get; set; }
        public string Endereco { get; set; }
        public string Cidade{ get; set; }
        public string Telefone{ get; set; }
        public string Email{ get; set; }
        public DateTime? DataInclusao { get; set; }
    }
}
