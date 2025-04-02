using TesteCarrefour.Entity;

namespace TesteCarrefour.Handler.Interface;
public interface ILancamentoHandler
{
    Task<Lancamento> CriarLancamentoAsync(LancamentoDados lancamento);
    Task<List<Lancamento>> ObterTodosLancamentosAsync();
    Task<Lancamento> ObterLancamentoPorIdAsync(int id);
}