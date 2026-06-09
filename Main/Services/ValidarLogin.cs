public class ValidarLogin
{
    public bool ValidarLogins(List<Login> usuarios, string email, string senha)
    {
        return usuarios.Any(u => u.Email == email && u.Senha == senha);
    }
}