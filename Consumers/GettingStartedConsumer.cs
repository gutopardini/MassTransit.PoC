using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Company.Consumers
{
    using System.Threading.Tasks;
    using MassTransit;
    using Contracts;

    public class GettingStartedConsumer :
        IConsumer<HelloMessage>
    {
        private readonly ILogger<GettingStartedConsumer> _logger;

        public GettingStartedConsumer(ILogger<GettingStartedConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<HelloMessage> context)
        {
            _logger.LogInformation("Tentando processar a {Value}", context.Message.Value);
            string fileName = @"/home/lpardini/RiderProjects/path/file.txt";
            if (!File.Exists(fileName))
            {
                throw new Exception("Deu ruim não existe");
            }

            using StreamWriter sw = File.AppendText(fileName);
            sw.WriteLine(context.Message.Value);
            return Task.CompletedTask;
        }
    }
}