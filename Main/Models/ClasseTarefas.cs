namespace TarefaClasse;

class Tarefa
{
    public string Descricao { get; set; } = string.Empty;
    public bool Concluida { get; set; } = false;
    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public string Impotancia { get; set; } = "Baixa";

    public Tarefa(string descricao)
    {
        Descricao = descricao;
        Impotancia = "Baixa";
        Concluida = false;
    }
}