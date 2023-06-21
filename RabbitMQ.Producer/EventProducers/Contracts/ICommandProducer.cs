using RabbitMQ.Producer.ETOs;

namespace RabbitMQ.Producer.EventProducers.Contracts
{
    public interface ICommandProducer
    {
        /// <summary>
        /// Publish the send sms command on rabbitmq
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task PublishSendSmsCommand(SendSmsCommand command, CancellationToken cancellationToken);
    }
}
