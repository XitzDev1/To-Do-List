class Tarefa
{
    public string Descricao { get; set; } = string.Empty;
    public bool Concluida { get; set; } = false;

    public Tarefa(string descricao)
    {
        Descricao = descricao;
        Concluida = false;
    }
}