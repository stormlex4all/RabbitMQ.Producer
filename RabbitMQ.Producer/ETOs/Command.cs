namespace RabbitMQ.Producer.ETOs
{
    public abstract class Command : IBaseEto
    {
        /// <summary>
        /// Name of the client service sending the command
        /// </summary>
        public string ClientServiceName => Utility.GetServiceName();
    }
}
