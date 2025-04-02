using TesteCarrefour.Entity;

namespace TesteCarrefour.Repository.Interface;

public interface IDatabaseRepository
{
    Task<Lancamento> CriarLancamentoAsync(Lancamento lancamento);
    Task<List<Lancamento>> ObterTodosLancamentosAsync();
    Task<Lancamento> ObterLancamentoPorIdAsync(int id);
    Task<List<Consolidado>> ObterConsolidado();
    Task<List<Consolidado>> ObterConsolidado(DateTime data);
}