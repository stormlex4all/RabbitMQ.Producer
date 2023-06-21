using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Producer.ETOs;
using RabbitMQ.Producer.EventProducers.Contracts;
using RabbitMQ.Producer.Settings;
using System.Text;
using ILogger = Serilog.ILogger;

namespace RabbitMQ.Producer.EventProducers
{
    public class CommandProducer : ICommandProducer
    {
        private readonly ILogger _logger;
        private readonly RabbitMQSettings _rabbitMQSettings;

        public CommandProducer(ILogger logger, IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _logger = logger;
            _rabbitMQSettings = rabbitMQSettings.Value;
        }

        /// <summary>
        /// Publish the send sms command on rabbitmq
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task PublishSendSmsCommand(SendSmsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(_rabbitMQSettings.ConnectionString)
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                string queueName = nameof(Enums.SendSmsCommandQueue);
                string exchangeName = nameof(Enums.SendSmsCommandQueueDirect);

                channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, durable: true, autoDelete: false);

                var body = Encoding.UTF8.GetBytes(command.Serialize());
                int count = 0;
                while (!cancellationToken.IsCancellationRequested)
                {
                    channel.BasicPublish(exchangeName, queueName, null, body);
                    Console.WriteLine("Published.." + ++count);
                    await Task.Delay(1000, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException) { throw; }
                _logger.Error(ex, ex.Message);
            }
        }
    }
}
