using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;

public static class LoginView
{
    public static Login? Run(List<Login> usuarios)
    {
        ReadLine readLine = new();
        bool usuarioLogado = false;
        Login? usuarioAtual = null;

        Console.WriteLine("Você tem uma conta? (s/n)");
        string respostaConta = readLine.Readline() ?? "";

        if (respostaConta == "s")
        {
            Console.WriteLine("Digite seu email:");
            string email = readLine.Readline() ?? "";

            Console.WriteLine("Digite sua senha:");
            string senha = readLine.Readline() ?? "";

            usuarioAtual = usuarios.FirstOrDefault(
                u => u.Email == email && u.Senha == senha
            );

            if (usuarioAtual == null)
            {
                Console.WriteLine("Email ou senha inválidos.");
                return null;
            }

            Console.WriteLine($"Bem-vindo, {usuarioAtual.Email}!");
            return usuarioAtual;
        }
        else
        {
            Console.WriteLine("Vamos criar sua conta.");

            Console.WriteLine("Digite seu email:");
            string email = readLine.Readline() ?? "";

            if (usuarios.Any(u => u.Email == email))
            {
                Console.WriteLine("Já existe uma conta com esse email.");
            }

            Console.WriteLine("Digite sua senha:");
            string senha = readLine.Readline() ?? "";

            Login novoLogin = new(email, senha);
            usuarios.Add(novoLogin);

            string json = JsonSerializer.Serialize(
                usuarios,
                new JsonSerializerOptions { WriteIndented = true }
            );
            File.WriteAllText("usuarios.json", json);

            Console.WriteLine("Conta criada com sucesso!");
            return novoLogin;
        }

        if (!usuarioLogado || usuarioAtual == null)
        {
            Console.WriteLine("Você precisa estar logado.");
            return null;
        }

        string arquivoTarefasJson = usuarioAtual.Email.Replace("@", "_").Replace(".", "_") + ".json";
        TarefaService tarefaService = new TarefaService(arquivoTarefasJson);

        Console.WriteLine("Digite 'help' para ver os comandos.");
}
}