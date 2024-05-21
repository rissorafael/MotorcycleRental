using Npgsql;

namespace MotorcycleRental.Domain.Interfaces
{
    public interface IConnectionFactory
    {
        NpgsqlConnection Connection();
    }
}
