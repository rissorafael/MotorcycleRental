using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IQueueService
    {
        Task ProcessaMenssagemAsync(TransacaoMensagem menssagem);
    }
}
