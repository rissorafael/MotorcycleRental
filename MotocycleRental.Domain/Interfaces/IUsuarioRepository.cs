using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> AddAsync(Usuario usuario);

        Task<Usuario> GetByUserNameAsync(string userName);
    }
}
