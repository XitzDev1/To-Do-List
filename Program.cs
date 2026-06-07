using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Main executado com sucesso!");

        Tarefa[]? Tarefas = new Tarefa[100];

        if (File.Exists("tarefas.txt"))
        {
            string[] tarefasSalvas = File.ReadAllLines("tarefas.txt");

            for (int i = 0; i < tarefasSalvas.Length; i++)
            {
                string[] partes = tarefasSalvas[i].Split('|');

                if (partes.Length == 2)
                {
                    Tarefas[i] = new Tarefa(partes[1]);
                    Tarefas[i].Concluida = bool.Parse(partes[0]);
                }
            }
        }

        Console.WriteLine("Bem-vindo a To-Do List!");
        Console.WriteLine("Digite 'help' para ver os comandos.");

        while (true)
        {
            Console.Write("> ");
            string resposta = Console.ReadLine() ?? "";

            if (resposta == "help")
            {
                Console.WriteLine("Comandos disponíveis:");
                Console.WriteLine("help     - Exibe esta mensagem");
                Console.WriteLine("add      - Adiciona uma tarefa");
                Console.WriteLine("list     - Lista as tarefas");
                Console.WriteLine("remove   - Remove uma tarefa");
                Console.WriteLine("complete - Marca uma tarefa como concluída");
                Console.WriteLine("exit     - Sai do programa");
            }

            else if (resposta == "add")
            {
                Console.WriteLine("Digite a tarefa:");
                string novaTarefa = (Console.ReadLine() ?? "").Trim();

                if (string.IsNullOrWhiteSpace(novaTarefa))
                {
                    Console.WriteLine("A tarefa não pode estar vazia.");
                    continue;
                }

                for (int i = 0; i < Tarefas.Length; i++)
                {
                    if (Tarefas[i] == null)
                    {
                        Tarefas[i] = new Tarefa(novaTarefa);
                        Console.WriteLine("Tarefa adicionada com sucesso!");
                        SalvarTarefas(Tarefas);
                        break;
                    }
                }
            }

            else if (resposta == "list")
            {
                Console.WriteLine("Tarefas:");

                for (int i = 0; i < Tarefas.Length; i++)
                {
                    if (Tarefas[i] != null)
                    {
                        string status = Tarefas[i].Concluida ? "[X]" : "[ ]";

                        Console.WriteLine(
                            $"{i + 1}. {status} {Tarefas[i].Descricao}"
                        );
                    }
                }
            }

            else if (resposta == "remove")
            {
                Console.WriteLine("Digite o número da tarefa:");

                if (int.TryParse(Console.ReadLine(), out int numeroTarefa))
                {
                    if (numeroTarefa > 0 &&
                        numeroTarefa <= Tarefas.Length &&
                        Tarefas[numeroTarefa - 1] != null)
                    {
                        Tarefas[numeroTarefa - 1] = null;

                        Console.WriteLine("Tarefa removida com sucesso!");

                        SalvarTarefas(Tarefas);
                    }
                    else
                    {
                        Console.WriteLine("Número de tarefa inválido.");
                    }
                }
                else
                {
                    Console.WriteLine("Digite um número válido.");
                }
            }

            else if (resposta == "complete")
            {
                Console.WriteLine("Digite o número da tarefa:");

                if (int.TryParse(Console.ReadLine(), out int numeroTarefa))
                {
                    if (numeroTarefa > 0 &&
                        numeroTarefa <= Tarefas.Length &&
                        Tarefas[numeroTarefa - 1] != null)
                    {
                        Tarefas[numeroTarefa - 1].Concluida = true;

                        Console.WriteLine(
                            "Tarefa marcada como concluída!"
                        );

                        SalvarTarefas(Tarefas);
                    }
                    else
                    {
                        Console.WriteLine("Número de tarefa inválido.");
                    }
                }
                else
                {
                    Console.WriteLine("Digite um número válido.");
                }
            }

            else if (resposta == "exit")
            {
                SalvarTarefas(Tarefas);
                break;
            }

            else
            {
                Console.WriteLine(
                    "Comando desconhecido. Digite 'help'."
                );
            }
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