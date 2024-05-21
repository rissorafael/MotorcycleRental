using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IPlanosRepository
    {
        Task<IEnumerable<Planos>> GetAllAsync();
        Task<Planos> GetByPlanoQuantidadeDiasAsync(int quantidadeDias);
        Task<Planos> GetByIdAsync(int id);
    }
}
