using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Main executado com sucesso!");

        List<Tarefa> tarefas = new();

        if (File.Exists("tarefas.txt"))
        {
            string[] tarefasSalvas = File.ReadAllLines("tarefas.txt");

            for (int i = 0; i < tarefasSalvas.Length; i++)
            {
                string[] partes = tarefasSalvas[i].Split('|');

                if (partes.Length == 2)
                {
                    Tarefa tarefa = new Tarefa(partes[1]);
                    tarefa.Concluida = bool.Parse(partes[0]);
                    tarefas.Add(tarefa);
                }
            }
        }

        Console.WriteLine("Bem-vindo a To-Do List!");
        Console.WriteLine("Digite 'help' para ver os comandos.");
        TarefaService tarefaService = new TarefaService();

        while (true)
{
    Console.Write("> ");
    string resposta = Console.ReadLine() ?? "";

    if (resposta == "add")
    {
        Console.Write("Descrição: ");
        string descricao = Console.ReadLine() ?? "";

        tarefaService.AdicionarTarefa(descricao);
    }

    else if (resposta == "list")
    {
        tarefaService.ListarTarefas();
    }

    else if (resposta == "remove")
    {
        Console.Write("Número da tarefa: ");

        if (int.TryParse(Console.ReadLine(), out int numero))
        {
            tarefaService.RemoverTarefa(numero);
        }
    }

    else if (resposta == "complete")
    {
        Console.Write("Número da tarefa: ");

        if (int.TryParse(Console.ReadLine(), out int numero))
        {
            tarefaService.CompletarTarefa(numero);
        }
    }

    else if (resposta == "exit")
    {
        break;
    }
    
    else if (resposta == "help")
    {
        Console.WriteLine("Comandos disponíveis:");
        Console.WriteLine("add - Adicionar uma nova tarefa");
        Console.WriteLine("list - Listar todas as tarefas");
        Console.WriteLine("remove - Remover uma tarefa pelo número");
        Console.WriteLine("complete - Marcar uma tarefa como concluída pelo número");
        Console.WriteLine("exit - Sair do programa");
    }

    else
    {
        Console.WriteLine("Comando desconhecido. Digite 'help' para ver os comandos disponíveis.");
    }
}

    static void SalvarTarefas(Tarefa[] tarefas)
    {
        File.WriteAllLines(
            "tarefas.txt",
            tarefas
                .Where(t => t != null)
                .Select(t => $"{t.Concluida}|{t.Descricao}")
        );
    }
}
}