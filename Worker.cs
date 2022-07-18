using System.Threading;
using System.Threading.Tasks;
using Company.Consumers;
using Contracts;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GettingStarted
{
    public class Worker : BackgroundService
    {
        private readonly IBus _bus;
        readonly ILogger<GettingStartedConsumer> _logger;

        public Worker(IBus bus, ILogger<GettingStartedConsumer> logger)
        {
            _bus = bus;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            for (int i = 1; i <= 10; i++)
            {
                await _bus.Publish(new HelloMessage
                {
                    Value = i.ToString()
                }, stoppingToken);
                _logger.LogInformation("Mandei mais um pra fila: {Value}", i.ToString() );
            }
            await Task.Delay(1, stoppingToken);
        }
    }
}