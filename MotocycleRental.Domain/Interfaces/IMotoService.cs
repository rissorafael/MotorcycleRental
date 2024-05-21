using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IMotoService
    {
        Task<List<MotoResponseModel>> GetAllAsync();
        Task<MotoResponseModel> GetByIdAsync(int id);
        Task<MotoResponseModel> AddAsync(MotoRequestModel request);
        Task<MotoResponseModel> GetPlacaMoto(string placa);
        Task<MotoResponseModel> UpdatePlacaAsync(int id, string placa);
        Task<MotoResponseModel> DeleteAsync(int id);
    }
}
