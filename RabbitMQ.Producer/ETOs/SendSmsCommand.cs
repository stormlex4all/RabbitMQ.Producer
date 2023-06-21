namespace RabbitMQ.Producer.ETOs
{
    public class SendSmsCommand : Command
    {
        /// <summary>
        /// Recipient phone number
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Text message to be sent
        /// </summary>
        public string SmsText { get; set; }

        /// <summary>
        /// Name of sms sender
        /// </summary>
        public string SmsSenderName { get; set; }
    }
}
