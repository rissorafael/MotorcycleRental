using Dapper;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;

namespace MotorcycleRental.Infra.Data.Repositories
{
    public class PlanosRepository : IPlanosRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public PlanosRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Planos>> GetAllAsync()
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryAsync<Planos>(@"
                                                SELECT 
                                                       id
                                                      ,valordiaria
                                                      ,quantidadedias
                                                      ,jurosdiariasnefetivadas
                                                      ,descricao
                                                   FROM Planos
                                                      Where 1=1");

            return result;
        }


        public async Task<Planos> GetByPlanoQuantidadeDiasAsync(int quantidadeDias)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstOrDefaultAsync<Planos>(@"
                                                SELECT 
                                                       id
                                                      ,valordiaria
                                                      ,quantidadedias
                                                      ,jurosdiariasnefetivadas
                                                      ,descricao
                                                   FROM Planos
                                                      Where quantidadedias = @quantidadedias", new
            {
                quantidadedias = quantidadeDias
            });

            return result;
        }

        public async Task<Planos> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstOrDefaultAsync<Planos>(@"
                                                SELECT 
                                                       id
                                                      ,valordiaria
                                                      ,quantidadedias
                                                      ,jurosdiariasnefetivadas
                                                      ,descricao
                                                   FROM Planos
                                                      Where id = @Id", new
            {
                Id = id
            });

            return result;
        }
    }
}
