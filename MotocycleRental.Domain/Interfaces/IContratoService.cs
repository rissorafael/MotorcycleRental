using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IContratoService
    {
        Task<ContratoResponseModel> AddAsync(int idLocacao, DateTime dataFim);
        void ContratoProducer(int idLocacao, DateTime dataFim);
    }
}
