using TesteCarrefour.Entity;
using TesteCarrefour.Handler.Interface;
using TesteCarrefour.Repository.Interface;

namespace TesteCarrefour.Handler;

public class LancamentoHandler : ILancamentoHandler
{
    private readonly IDatabaseRepository _repository;

    public LancamentoHandler(IDatabaseRepository repository) => _repository = repository;

    public async Task<Lancamento> CriarLancamentoAsync(LancamentoDados lancamentoDados)
    {
        Lancamento lancamento = new()
        {
            Valor = lancamentoDados.Valor,
            DataLancamento = lancamentoDados.DataLancamento,
            Tipo = lancamentoDados.Tipo
        };

        return await _repository.CriarLancamentoAsync(lancamento);
    }
       

    public async Task<List<Lancamento>> ObterTodosLancamentosAsync() => 
        await _repository.ObterTodosLancamentosAsync();

    public async Task<Lancamento> ObterLancamentoPorIdAsync(int id) => 
        await _repository.ObterLancamentoPorIdAsync(id) ?? throw new KeyNotFoundException("Lançamento não encontrado.");
}