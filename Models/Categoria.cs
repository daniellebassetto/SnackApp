namespace SnackApp.Models;

public class Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }

    public List<Lanche> Lanches { get; set; }
}