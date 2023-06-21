namespace RabbitMQ.ETOs
{
    public class IBaseEto
    {
        /// <summary>
        /// Name of the client service sending the command (for internal use)
        /// </summary>
        public string ClientServiceName { get; }
    }
}