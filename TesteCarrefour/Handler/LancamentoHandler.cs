using Polly;
using Polly.Retry;
using Polly.CircuitBreaker;
using TesteCarrefour.Entity;
using TesteCarrefour.Handler.Interface;
using TesteCarrefour.Repository.Interface;

namespace TesteCarrefour.Handler;

public class LancamentoHandler : ILancamentoHandler
{
    private readonly IDatabaseRepository _repository;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;

    public LancamentoHandler(IDatabaseRepository repository)
    {
        _repository = repository;

        // Retry: Tenta novamente até 3 vezes com um atraso exponencial
        _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(500 * attempt));

        // Circuit Breaker: Após 5 falhas consecutivas, bloqueia chamadas por 10 segundos
        _circuitBreakerPolicy = Policy
            .Handle<Exception>()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(10));
    }

    public async Task<Lancamento> CriarLancamentoAsync(LancamentoDados lancamentoDados)
    {
        Lancamento lancamento = new()
        {
            Valor = lancamentoDados.Valor,
            DataLancamento = lancamentoDados.DataLancamento,
            Tipo = lancamentoDados.Tipo
        };

        return await _retryPolicy.ExecuteAsync(() =>
            _circuitBreakerPolicy.ExecuteAsync(() =>
                _repository.CriarLancamentoAsync(lancamento)
            ));
    }

    public async Task<List<Lancamento>> ObterTodosLancamentosAsync() =>
        await _retryPolicy.ExecuteAsync(() =>
            _circuitBreakerPolicy.ExecuteAsync(() =>
                _repository.ObterTodosLancamentosAsync()
            ));

    public async Task<Lancamento> ObterLancamentoPorIdAsync(int id) =>
        await _retryPolicy.ExecuteAsync(() =>
            _circuitBreakerPolicy.ExecuteAsync(async () =>
                await _repository.ObterLancamentoPorIdAsync(id)
                ?? throw new KeyNotFoundException("Lançamento não encontrado.")
            ));
}