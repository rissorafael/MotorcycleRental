using Dapper;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;


namespace MotorcycleRental.Infra.Data.Repositories
{
    public class MotoRepository : IMotoRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public MotoRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Moto>> GetAllAsync()
        {
            try
            {
                using var connection = _connectionFactory.Connection();
                var result = await connection.QueryAsync<Moto>(@"
                                                SELECT 
                                                       id
                                                      ,modelo
                                                      ,marca
                                                      ,ano
                                                      ,placa
                                                      ,cilindradas
                                                    FROM Moto
                                                      Where 1=1");

                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<Moto> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstOrDefaultAsync<Moto>(@"
                                                SELECT 
                                                       id
                                                      ,modelo
                                                      ,marca
                                                      ,ano
                                                      ,placa
                                                      ,cilindradas
                                                    FROM Moto
                                                      Where Id =@id",
                new
                {
                    Id = id
                });

            return result;

        }

        public async Task<Moto> GetByPlacaAsync(string placa)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstOrDefaultAsync<Moto>(@"
                                                SELECT 
                                                       id
                                                      ,modelo
                                                      ,marca
                                                      ,ano
                                                      ,placa
                                                      ,cilindradas
                                                    FROM Moto
                                                      Where Placa =@placa",
    new
    {
        Placa = placa
    });

            return result;

        }

        public async Task<Moto> AddAsync(Moto motocycle)
        {

            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstAsync<Moto>(@"
                              
                              INSERT INTO  Moto
                              (
                                 modelo
                                ,marca
                                ,ano 
                                ,placa
                                ,cilindradas
                              )
                             VALUES 
                             (
                                 @modelo
                                ,@marca
                                ,@ano
                                ,@placa
                                ,@cilindradas
                              )
                           RETURNING 
                                id
                               ,modelo
                               ,marca
                               ,ano
                               ,placa
                               ,cilindradas
                             ;",
                new
                {
                    motocycle.Modelo,
                    motocycle.Marca,
                    motocycle.Ano,
                    motocycle.Placa,
                    motocycle.Cilindradas
                });

            return result;

        }

        public async Task<Moto> UpdatePlacaAsync(int id, string placa)
        {

            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstAsync<Moto>(@"
                              
                              UPDATE Moto
                              SET Placa = @placa
                                WHERE Id = @id
                           RETURNING 
                                id
                               ,modelo
                               ,marca
                               ,ano
                               ,placa
                               ,cilindradas
                             ;",
                new
                {
                    Id = id,
                    Placa = placa
                });

            return result;
        }

        public async Task<int> DeleteAsync(int id)
        {

            using var connection = _connectionFactory.Connection();
            var result = await connection.ExecuteAsync(@"
                              
                              DELETE FROM  Moto
                                 WHERE Id = @id
                            ;",
                new
                {
                    Id = id
                });

            return result;

        }
    }
}
