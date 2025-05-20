using Contato.Delete.Worker.Infra.Mensageria.Consumer;

namespace Contato.Delete.Worker.Service
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IContatoConsumer _consumer;
        private readonly IServiceProvider _serviceProvider;
        public Worker(ILogger<Worker> logger, IContatoConsumer consumer, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _consumer = consumer;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var consumer = scope.ServiceProvider.GetRequiredService<IContatoConsumer>();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                _consumer.StartConsuming(stoppingToken);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
