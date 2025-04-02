using System.Text;
using RabbitMQ.Client;

namespace TesteCarrefour.Services.Rabbit;

public class RabbitMqPublisher : IAsyncDisposable
{
    private readonly ConnectionFactory _factory;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly string _queueName = "minha_fila";

    public RabbitMqPublisher(string hostName = "localhost")
    {
        _factory = new ConnectionFactory
        {
            HostName = hostName,
            AutomaticRecoveryEnabled = true
        };
    }

    public async Task ConectarAsync()
    {
        _connection = await _factory.CreateConnectionAsync();
        _channel = await _connection.CreateChannelAsync();

        var result = _channel.QueueDeclareAsync(queue: _queueName,
                                           durable: true,
                                           exclusive: false,
                                           autoDelete: false,
                                           arguments: new Dictionary<string, object>());
    }

    public async Task PostarMensagemAsync(string mensagem)
    {
        if (_channel == null)
            await ConectarAsync();

        var body = Encoding.UTF8.GetBytes(mensagem);

        await _channel.BasicPublishAsync(exchange: "",
                                         routingKey: _queueName,
                                         body: body);

        Console.WriteLine($" [x] Mensagem enviada: {mensagem}");
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (_channel != null)
        {
            await _channel.DisposeAsync();
        }
        if (_connection != null)
        {
            await _connection.DisposeAsync();
        }
    }
}