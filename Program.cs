using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;

class ProgramToDoList
{
    static void Main(string[] args)
    {
        Console.WriteLine("Bem-vindo à To-Do List!");

        bool usuarioLogado = false;
        Login? usuarioAtual = null;

        List<Login> usuarios = new();

        if (File.Exists("usuarios.json"))
        {
            string json = File.ReadAllText("usuarios.json");
            usuarios = JsonSerializer.Deserialize<List<Login>>(json) ?? new();
        }

        Console.WriteLine("Você tem uma conta? (s/n)");
        string respostaConta = Console.ReadLine()?.ToLower() ?? "";

        if (respostaConta == "s")
        {
            Console.WriteLine("Digite seu email:");
            string email = Console.ReadLine() ?? "";

            Console.WriteLine("Digite sua senha:");
            string senha = Console.ReadLine() ?? "";

            usuarioAtual = usuarios.FirstOrDefault(
                u => u.Email == email && u.Senha == senha
            );

            if (usuarioAtual == null)
            {
                Console.WriteLine("Email ou senha inválidos.");
                return;
            }

            usuarioLogado = true;

            Console.WriteLine($"Bem-vindo, {usuarioAtual.Email}!");
        }
        else
        {
            Console.WriteLine("Vamos criar sua conta.");

            Console.WriteLine("Digite seu email:");
            string email = Console.ReadLine() ?? "";

            if (usuarios.Any(u => u.Email == email))
            {
                Console.WriteLine("Já existe uma conta com esse email.");
                return;
            }

            Console.WriteLine("Digite sua senha:");
            string senha = Console.ReadLine() ?? "";

            Login novoLogin = new Login(email, senha);

            usuarios.Add(novoLogin);

            string json = JsonSerializer.Serialize(
                usuarios,
                new JsonSerializerOptions
                {
                    WriteIndented = true
                });

            File.WriteAllText("usuarios.json", json);

            usuarioAtual = novoLogin;
            usuarioLogado = true;

            Console.WriteLine($"Conta criada com sucesso!");
        }

        if (!usuarioLogado || usuarioAtual == null)
        {
            Console.WriteLine("Você precisa estar logado.");
            return;
        }

        string arquivoTarefas =
            usuarioAtual.Email.Replace("@", "_").Replace(".", "_") + ".txt";

        List<Tarefa> tarefas = new();

        if (File.Exists(arquivoTarefas))
        {
            string[] tarefasSalvas = File.ReadAllLines(arquivoTarefas);

            foreach (string linha in tarefasSalvas)
            {
                string[] partes = linha.Split('|');

                if (partes.Length == 2)
                {
                    Tarefa tarefa = new Tarefa(partes[1]);
                    tarefa.Concluida = bool.Parse(partes[0]);

                    tarefas.Add(tarefa);
                }
            }
        }

        string arquivoTarefas1 =
    usuarioAtual.Email.Replace("@", "_")
                      .Replace(".", "_")
    + ".json";

    TarefaService tarefaService =
    new TarefaService(arquivoTarefas);

        Console.WriteLine("Digite 'help' para ver os comandos.");

        while (true)
        {
            Console.Write("> ");
            string comando = Console.ReadLine() ?? "";

            if (comando == "add")
            {
                Console.Write("Descrição: ");
                string descricao = Console.ReadLine() ?? "";

                tarefaService.AdicionarTarefa(descricao);
            }
            else if (comando == "list")
            {
                tarefaService.ListarTarefas();
            }
            else if (comando == "remove")
            {
                Console.Write("Número da tarefa: ");

                if (int.TryParse(Console.ReadLine(), out int numero))
                {
                    tarefaService.RemoverTarefa(numero);
                }
            }
            else if (comando == "complete")
            {
                Console.Write("Número da tarefa: ");

                if (int.TryParse(Console.ReadLine(), out int numero))
                {
                    tarefaService.CompletarTarefa(numero);
                }
            }
            else if (comando == "help")
            {
                Console.WriteLine("add");
                Console.WriteLine("list");
                Console.WriteLine("remove");
                Console.WriteLine("complete");
                Console.WriteLine("exit");
            }
            else if (comando == "exit")
            {
                break;
            }
            else
            {
                Console.WriteLine("Comando desconhecido.");
            }
        }
    }
}