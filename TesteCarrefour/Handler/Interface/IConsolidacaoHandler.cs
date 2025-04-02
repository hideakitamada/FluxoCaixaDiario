using TesteCarrefour.Entity;

namespace TesteCarrefour.Handler.Interface;

public interface IConsolidacaoHandler
{
    Task<List<Consolidado>> ObterConsolidado();
    Task<List<Consolidado>> ObterConsolidadoPorData(DateTime date);
}