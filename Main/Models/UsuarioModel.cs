namespace UsuarioModel;
public class Usuario(string email, string senha, string nome = "")
{
    public string Nome { get; set; } = nome;
    public string Email { get; set; } = email;
    public string Senha { get; set; } = senha;
}