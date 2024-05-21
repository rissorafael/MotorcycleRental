using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface ILocacaoRepository
    {
        Task<Locacao> UpdateStatusAsync(int id, string status);
        Task<Locacao> GetByIdAsync(int id);
        Task<IEnumerable<Locacao>> GetByMotoIdAsync(int motoId);
        Task<Locacao> AddAsync(Locacao locacao);
    }
}
