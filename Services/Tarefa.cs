using System.Text.Json;

public class TarefaService
{
    private readonly string arquivoTarefas;
    private List<Tarefa> tarefas;

    public TarefaService(string arquivoTarefas)
    {
        this.arquivoTarefas = arquivoTarefas;

        if (File.Exists(arquivoTarefas))
        {
            string json = File.ReadAllText(arquivoTarefas);

            tarefas = JsonSerializer.Deserialize<List<Tarefa>>(json)
                      ?? new List<Tarefa>();
        }
        else
        {
            tarefas = new List<Tarefa>();
        }
    }

    public void AdicionarTarefa(string descricao)
    {
        tarefas.Add(new Tarefa(descricao));
        Salvar();
    }

    public void ListarTarefas()
    {
        if (tarefas.Count == 0)
        {
            Console.WriteLine("Nenhuma tarefa cadastrada.");
            return;
        }

        for (int i = 0; i < tarefas.Count; i++)
        {
            var t = tarefas[i];
            Console.WriteLine($"{i + 1}. [{(t.Concluida ? "Concluída" : "Pendente")}] {t.Descricao}");
        }
    }

    public void RemoverTarefa(int index)
    {
        tarefas.RemoveAt(index - 1);
        Salvar();
    }

    public void CompletarTarefa(int index)
    {
        tarefas[index - 1].Concluida = true;
        Salvar();
    }

    private void Salvar()
    {
        string json = JsonSerializer.Serialize(tarefas);
        File.WriteAllText(arquivoTarefas, json);
    }
}