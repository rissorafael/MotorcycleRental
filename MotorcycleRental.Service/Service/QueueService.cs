using Microsoft.Extensions.Logging;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;
using System.Text.Json;


namespace MotorcycleRental.Service.Service
{
    public class QueueService : IQueueService
    {
        private readonly ILogger<QueueService> _logger;
        private readonly IContratoService _contratoService;

        public QueueService(ILogger<QueueService> logger, IContratoService contratoService)
        {
            _logger = logger;
            _contratoService = contratoService;
        }

        public async Task ProcessaMenssagemAsync(TransacaoMensagem menssagem)
        {
            try
            {
                _logger.LogWarning($"[QueueService - ProcessaMenssagemAsync] Mensagem recebida - {JsonSerializer.Serialize(menssagem)}");

                await _contratoService.AddAsync(menssagem.IdLocacao, menssagem.dataFim);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[QueueService - ProcessaMenssagemAsync] Error:  {JsonSerializer.Serialize(ex.Message)}");
                throw;
            }
        }
    }
}
