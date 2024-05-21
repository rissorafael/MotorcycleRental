using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IEntregadorRepository
    {
        Task<Entregador> AddAsync(Entregador entregador);
        Task<Entregador> GetByCnpjOrNumeroCnhAsync(string cnpj, long numeroCnh);
        Task<Entregador> GetByIdAsync(int id);
    }
}
