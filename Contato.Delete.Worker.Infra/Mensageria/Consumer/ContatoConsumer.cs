using Contato.Delete.Worker.Application.Dtos;
using Contato.Delete.Worker.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Contato.Delete.Worker.Infra.Mensageria.Consumer
{
    public class ContatoConsumer : IContatoConsumer, IDisposable
    {
        private readonly IModel _channel;
        private readonly IConnection _connection;
        private readonly IContatoAppService _appService;

        public ContatoConsumer(IContatoAppService appService, IConfiguration configuration, IConnection rabbitConnection)
        {
            _appService = appService;
            _connection = rabbitConnection;
            _channel = _connection.CreateModel();

            var queueName = configuration["RabbitMQ:QueueName"] ?? "deletar-contato";

            _channel.QueueDeclare(queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }
        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }

        public void StartConsuming(CancellationToken cancellationToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    Console.WriteLine($"Mensagem recebida: {message}");

                    var dto = JsonConvert.DeserializeObject<DeletarContatoDto>(message);

                    _appService.DeletarContato(dto);
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
                }
   
            };

            _channel.BasicConsume(queue: "deletar-contato", autoAck: true, consumer: consumer);
        }
    }
}
