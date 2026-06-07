public class Login
{
    public string Email { get; set; }
    public string Senha { get; set; }

    public Login(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }

    public bool ValidarLogin(List<Login> usuarios)
    {
        return usuarios.Any(u => u.Email == Email && u.Senha == Senha);
    }
}