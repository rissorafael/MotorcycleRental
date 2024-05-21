
using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IContratoRepository
    {
        Task<Contrato> GetByIdLocacaoAsync(int idLocacao);
        Task<Contrato> AddAsync(Contrato contrato);
    }
}
