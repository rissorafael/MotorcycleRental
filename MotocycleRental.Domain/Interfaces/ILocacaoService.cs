using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface ILocacaoService
    {
        Task<LocacaoResponseModel> GetByIdAsync(int id);
        Task<List<LocacaoResponseModel>> GetByMotoIdAsync(int motoId);
        Task<LocacaoResponseModel> AddAsync(LocacaoRequestModel request);
        Task<LocacaoResponseModel> UpdateStatusAsync(int id, string status);
    }
}
