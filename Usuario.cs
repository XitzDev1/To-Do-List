public class Login
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }

    public Login(string email, string senha, string nome = "")
    {
        Email = email;
        Senha = senha;
        Nome = nome;
    }
}