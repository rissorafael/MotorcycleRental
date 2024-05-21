using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IPlanosService
    {
        Task<List<PlanosResponseModel>> GetAllAsync();
        Task<(double ValorTotal, int QuantidadesDias)> GetCauculoPlanosAsync(int planoQtdDias, DateTime dataInicio, DateTime? dataFim);
        Task<PlanosResponseModel> GetByIdAsync(int id);
    }
}
