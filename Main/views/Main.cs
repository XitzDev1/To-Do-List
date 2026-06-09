using System;

public static class ViewMain
{
    public static void Run(TarefaService tarefaService)
    {
        ReadLine readLine = new();

        while (true)
        {
            Console.Write("> ");
            string comando = readLine.Readline() ?? "";

            if (comando == "add")
            {
                Console.Write("Descrição: ");
                string descricao = readLine.Readline() ?? "";
                tarefaService.AdicionarTarefa(descricao);
            }
            else if (comando == "list")
            {
                tarefaService.ListarTarefas();
            }
            else if (comando == "remove")
            {
                Console.Write("Número da tarefa: ");
                if (int.TryParse(readLine.Readline(), out int numero))
                {
                    tarefaService.RemoverTarefa(numero);
                }
                else
                {
                    Console.WriteLine("Número inválido.");
                }
            }
            else if (comando == "complete")
            {
                Console.Write("Número da tarefa: ");
                if (int.TryParse(readLine.Readline(), out int numero))
                {
                    tarefaService.CompletarTarefa(numero);
                }
                else
                {
                    Console.WriteLine("Número inválido.");
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
