namespace Web.Api.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }

    public class LoginModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }

}
