using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IRolesRepository
    {
        Task<IEnumerable<Roles>> GetByUsuarioIdAsync(int usuarioId);
        Task<Roles> AddAsync(Roles roles);
    }
}
