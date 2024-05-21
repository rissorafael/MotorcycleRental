using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using System.Text;
using RabbitMQ.Client;
using MotorcycleRental.Domain.Enums;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace MotorcycleRental.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<Worker> _logger;

        public Worker(IServiceScopeFactory serviceScopeFactory, ILogger<Worker> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogWarning($"[Worker - CONSUMER] - {RabbitmqNames.PROCESSA_CONTRATO} inicializado.");

            stoppingToken.Register(() =>
            {
                _logger.LogWarning($"[Worker - CONSUMER] - {RabbitmqNames.PROCESSA_CONTRATO} esta parado.");
            });

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: RabbitmqNames.PROCESSA_CONTRATO, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var transacao = JsonSerializer.Deserialize<TransacaoMensagem>(message);

                    using var scope = _serviceScopeFactory.CreateScope();
                    try
                    {
                        var queueService = scope.ServiceProvider.GetRequiredService<IQueueService>();
                        await queueService.ProcessaMenssagemAsync(transacao);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"[Worker - CONSUMER] - Erro ao consumir a fila :{ex.Message}");
                    }
                };

                channel.BasicConsume(queue: RabbitmqNames.PROCESSA_CONTRATO, autoAck: true, consumer: consumer);

                await Task.Delay(Timeout.Infinite, stoppingToken);
                _logger.LogWarning($"[Worker - CONSUMER] - {RabbitmqNames.PROCESSA_CONTRATO} registrado.");
            }
        }
    }
}