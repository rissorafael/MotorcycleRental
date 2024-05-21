using MotorcycleRental.Domain.Models;


namespace MotorcycleRental.Domain.Interfaces
{
    public interface IRolesService
    {
        Task<List<RolesResponseModel>> GetByUsuarioIdAsync(int usuarioId);

        Task<RolesResponseModel> AddAsync(RolesRequestModel request);
    }
}
