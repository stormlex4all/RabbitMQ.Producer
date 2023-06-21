using RabbitMQ.Producer.EventProducers;
using RabbitMQ.Producer.EventProducers.Contracts;
using RabbitMQ.Producer.Settings;
using Serilog;

namespace RabbitMQ.Producer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<RabbitMQProducerWorker>();
                    services.AddSingleton<ICommandProducer, CommandProducer>();
                    services.Configure<RabbitMQSettings>(context.Configuration.GetSection(RabbitMQSettings.rabbitMQSettings));

                })
                .UseSerilog((context, services, configuration) => configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .ReadFrom.Services(services)
                    .Enrich.FromLogContext()
                    .WriteTo.Console())
                .Build();

            host.Run();
        }
    }
}