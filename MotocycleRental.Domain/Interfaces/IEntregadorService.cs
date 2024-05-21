using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IEntregadorService
    {
        Task<EntregadorResponseModel> AddAsync(EntregadorRequestModel request);
        Task<EntregadorResponseModel> GetByIdAsync(int id);
    }
}
