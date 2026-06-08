using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class TarefaService
{
    private readonly string arquivoTarefas;
    private List<Tarefa> tarefas;

    public TarefaService(string arquivoTarefas)
    {
        this.arquivoTarefas = arquivoTarefas;
        tarefas = new List<Tarefa>();
    }

    public void AdicionarTarefa(string descricao)
    {
        tarefas.Add(new Tarefa(descricao));
        Console.WriteLine("Tarefa adicionada com sucesso!");
        SalvarTarefas(tarefas.ToArray());
    }

    private void SalvarTarefas(Tarefa[] tarefas)
    {
        string json = JsonSerializer.Serialize(tarefas);
        File.WriteAllText(arquivoTarefas, json);
    }

    public void ListarTarefas()
    {
        if (tarefas.Count == 0 || tarefas == null)
        {
            Console.WriteLine("Nenhuma tarefa cadastrada.");
            return;
        }

        Console.WriteLine("Tarefas:");
        for (int i = 0; i < tarefas.Count; i++)
        {
            var tarefa = tarefas[i];
            if (tarefa != null)
            {
                string status = tarefa.Concluida ? "Concluída" : "Pendente";
                Console.WriteLine($"{i + 1}. [{status}] {tarefa.Descricao}");
            }
        }
}

    public void RemoverTarefa(int numeroTarefa)
    {
        if (numeroTarefa > 0 && numeroTarefa <= tarefas.Count && tarefas[numeroTarefa - 1] != null)
        {
            tarefas.RemoveAt(numeroTarefa - 1);
            Console.WriteLine("Tarefa removida com sucesso!");
            SalvarTarefas(tarefas.ToArray());
        }
        else
        {
            Console.WriteLine("Número de tarefa inválido.");
        }
    }

    public void CompletarTarefa(int numeroTarefa)
    {
        if (numeroTarefa > 0 && numeroTarefa <= tarefas.Count && tarefas[numeroTarefa - 1] != null)
        {
            tarefas[numeroTarefa - 1].Concluida = true;
            Console.WriteLine("Tarefa marcada como concluída!");
            SalvarTarefas(tarefas.ToArray());
        }
        else
        {
            Console.WriteLine("Número de tarefa inválido.");
        }
    }
}