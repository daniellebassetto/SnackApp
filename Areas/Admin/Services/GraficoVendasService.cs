using SnackApp.Context;
using SnackApp.Models;

namespace SnackApp.Areas.Admin.Services;

public class GraficoVendasService(SnackAppContext context)
{
    private readonly SnackAppContext context = context;

    public List<LancheGrafico> GetVendasLanches(int dias = 360)
    {
        var data = DateTime.Now.AddDays(-dias);

        var lanches = from pd in context.PedidoItens
                      join l in context.Lanches on pd.LancheId equals l.Id
                      where pd.Pedido.Data >= data
                      group pd by new { pd.LancheId, l.Nome }
                       into g
                      select new
                      {
                          LancheNome = g.Key.Nome,
                          LanchesQuantidade = g.Sum(q => q.Quantidade),
                          LanchesValorTotal = g.Sum(a => a.Preco * a.Quantidade)
                      };

        var lista = new List<LancheGrafico>();

        foreach (var item in lanches)
        {
            var lanche = new LancheGrafico
            {
                LancheNome = item.LancheNome,
                LanchesQuantidade = item.LanchesQuantidade,
                LanchesValorTotal = item.LanchesValorTotal
            };
            lista.Add(lanche);
        }
        return lista;
    }
}
