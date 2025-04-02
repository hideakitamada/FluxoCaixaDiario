using Polly;
using Polly.Retry;
using Polly.CircuitBreaker;
using TesteCarrefour.Entity;
using TesteCarrefour.Handler.Interface;
using TesteCarrefour.Repository.Interface;

namespace TesteCarrefour.Handler;

public class ConsolidacaoHandler : IConsolidacaoHandler
{
    private readonly IDatabaseRepository _repository;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;

    public ConsolidacaoHandler(IDatabaseRepository repository)
    {
        _repository = repository;

        // Política de Retry (Tenta novamente até 3 vezes com um pequeno atraso)
        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(500));

        // Política de Circuit Breaker (Se houver 5 falhas consecutivas, pausa as chamadas por 10 segundos)
        _circuitBreakerPolicy = Policy
            .Handle<Exception>()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(10));
    }

    public async Task<List<Consolidado>> ObterConsolidado() =>
        await _retryPolicy.ExecuteAsync(() =>
            _circuitBreakerPolicy.ExecuteAsync(() =>
                _repository.ObterConsolidado()
            ));

    public async Task<List<Consolidado>> ObterConsolidadoPorData(DateTime date) =>
        await _retryPolicy.ExecuteAsync(() =>
            _circuitBreakerPolicy.ExecuteAsync(() =>
                _repository.ObterConsolidado(date)
            ));
}
