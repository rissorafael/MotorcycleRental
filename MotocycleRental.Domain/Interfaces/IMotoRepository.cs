using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IMotoRepository
    {
        Task<IEnumerable<Moto>> GetAllAsync();
        Task<Moto> GetByIdAsync(int id);
        Task<Moto> AddAsync(Moto motocycle);
        Task<Moto> GetByPlacaAsync(string placa);
        Task<Moto> UpdatePlacaAsync(int id, string placa);
        Task<int> DeleteAsync(int id);
    }
}
