using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponseModel> AddAsync(UsuarioRequestModel request);
        Task<AutenticacaoResponseModel> LoginAsync(AutenticacaoRequestModel request);
        Task<UsuarioResponseModel> GetByUserNameAsync(string userName);
    }
}
