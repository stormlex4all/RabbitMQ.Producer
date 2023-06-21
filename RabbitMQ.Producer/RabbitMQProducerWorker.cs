using RabbitMQ.Producer.ETOs;
using RabbitMQ.Producer.EventProducers.Contracts;
using ILogger = Serilog.ILogger;

namespace RabbitMQ.Producer
{
    public class RabbitMQProducerWorker : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ICommandProducer _commandProducer;

        public RabbitMQProducerWorker(ILogger logger, ICommandProducer commandProducer)
        {
            _logger = logger;
            _commandProducer = commandProducer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.Information("Worker running at: {time}", DateTimeOffset.Now);
                await PerformSmsCommandPublish(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
            }
        }

        private async Task PerformSmsCommandPublish(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            SendSmsCommand command = new()
            {
                PhoneNumber = "08135081036",
                SmsText = "Test",
                SmsSenderName = "SmsMicroserviceTests"
            };

            await _commandProducer.PublishSendSmsCommand(command, stoppingToken);
        }
    }
}