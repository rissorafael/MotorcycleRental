
using System.Security.Claims;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(List<Claim> claims);
    }
}
