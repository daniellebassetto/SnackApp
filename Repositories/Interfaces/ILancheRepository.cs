using SnackApp.Models;

namespace SnackApp.Repositories.Interfaces;

public interface ILancheRepository
{
    IEnumerable<Lanche> Lanches {  get; }
    IEnumerable<Lanche> LanchesPreferidos {  get; }
    Lanche GetById(int id);
}