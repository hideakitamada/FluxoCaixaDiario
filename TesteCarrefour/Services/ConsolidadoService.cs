using Polly.Retry;
using Polly;
using RabbitMQ.Client;
using System.Text;
using TesteCarrefour.Entity;
using TesteCarrefour.Entity.Enum;

namespace TesteCarrefour.Services;

public class ConsolidadoService
{
    private readonly LancamentoDbContext _context;
    private readonly AsyncRetryPolicy _retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

    public ConsolidadoService(LancamentoDbContext context) => _context = context;

    public async Task ConsumirMensagens()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();
        await channel.QueueDeclareAsync(queue: "lancamentosQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

        //var consumer = new AsyncEventingBasicConsumer(channel);
        //consumer.ReceivedAsync += (model, ea) =>
        //{
        //    var body = ea.Body.ToArray();
        //    var message = Encoding.UTF8.GetString(body);
        //    var lancamento = System.Text.Json.JsonSerializer.Deserialize<Lancamento>(message);
        //    ProcessarLancamento(lancamento);
        //};
        //channel.BasicConsumeAsync(queue: "lancamentosQueue", autoAck: true, consumer: consumer);
    }
    private async Task PublicarNaFila(Lancamento lancamento)
    {
        await _retryPolicy.ExecuteAsync(async () =>
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            await channel.QueueDeclareAsync(queue: "lancamentosQueue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(lancamento));
            //await channel.BasicPublishAsync(exchange: "", routingKey: "lancamentosQueue", mandatory: true, basicProperties: null, body: body);
        });
    }

    private void ProcessarLancamento(Lancamento lancamento)
    {
        var totalCredito = _context.Lancamentos.Where(l => l.Tipo == TipoLancamento.Credito).Sum(l => l.Valor);
        var totalDebito = _context.Lancamentos.Where(l => l.Tipo == TipoLancamento.Debito).Sum(l => l.Valor);
        var saldo = totalCredito - totalDebito;

        Console.WriteLine($"Saldo atualizado: {saldo}");
    }
}