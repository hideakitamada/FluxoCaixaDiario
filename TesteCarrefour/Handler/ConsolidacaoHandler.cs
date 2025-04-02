using TesteCarrefour.Entity;
using TesteCarrefour.Handler.Interface;
using TesteCarrefour.Repository.Interface;

namespace TesteCarrefour.Handler;

public class ConsolidacaoHandler : IConsolidacaoHandler
{
    private readonly IDatabaseRepository _repository;

    public ConsolidacaoHandler(IDatabaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Consolidado>> ObterConsolidado() =>
        await _repository.ObterConsolidado();

    public async Task<List<Consolidado>> ObterConsolidadoPorData(DateTime date) => 
        await _repository.ObterConsolidado(date);
}