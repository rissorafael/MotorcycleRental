using MotorcycleRental.Domain.Enums;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using Microsoft.Extensions.Logging;

namespace MotorcycleRental.Service.Service
{
    public class Producer : IProducer
    {

        private readonly ILogger<Producer> _logger;

        public Producer(ILogger<Producer> logger)
        {
            _logger = logger;
        }

        public void PublicaMenssagem(TransacaoMensagem menssagem)
        {
            try
            {
                _logger.LogWarning($"[PublicaMenssagem] Publicado na fila - {RabbitmqNames.PROCESSA_CONTRATO} : {JsonSerializer.Serialize(menssagem)}");

                var json = JsonSerializer.Serialize(menssagem);

                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: RabbitmqNames.PROCESSA_CONTRATO,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);


                    var body = Encoding.UTF8.GetBytes(json);

                    channel.BasicPublish(exchange: "",
                                         routingKey: RabbitmqNames.PROCESSA_CONTRATO,
                                         basicProperties: null,
                                         body: body);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"[PublicaMenssagem] - Erro ao publicar na fila {RabbitmqNames.PROCESSA_CONTRATO} : {JsonSerializer.Serialize(ex.Message)}");
                throw;
            }
        }
    }
}
