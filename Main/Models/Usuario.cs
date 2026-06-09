public class Login
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;

    public Login(string email, string senha, string nome = "")
    {
        Email = email;
        Senha = senha;
        Nome = nome;
    }
}
