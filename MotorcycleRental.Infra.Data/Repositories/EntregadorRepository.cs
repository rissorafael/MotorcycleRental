using Dapper;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;

namespace MotorcycleRental.Infra.Data.Repositories
{
    public class EntregadorRepository : IEntregadorRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public EntregadorRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Entregador> AddAsync(Entregador entregador)
        {

            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstAsync<Entregador>(@"
                              
                              INSERT INTO  Entregador
                              (
                                 cnpj
                                ,numerocnh
                                ,tipocnh
                                ,imagemid
                                ,usuarioId
                              )
                             VALUES 
                             (
                                 @cnpj
                                ,@numerocnh
                                ,@tipocnh
                                ,@imagemid
                                ,@usuarioId
                              )
                           RETURNING 
                                id
                               ,cnpj
                               ,numerocnh
                               ,tipocnh
                               ,imagemid
                               ,usuarioId
                             ;",
                new
                {
                    entregador.Cnpj,
                    entregador.NumeroCnh,
                    entregador.TipoCnh,
                    entregador.ImagemId,
                    entregador.UsuarioId
                });

            return result;
        }


        public async Task<Entregador> GetByCnpjOrNumeroCnhAsync(string cnpj, long numeroCnh)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstOrDefaultAsync<Entregador>(@"
                                                SELECT 
                                                       id
                                                      ,cnpj
                                                      ,numerocnh
                                                      ,tipocnh
                                                      ,imagemid
                                                    FROM Entregador
                                                      Where Cnpj = @Cnpj 
                                                      Or numerocnh = @NumeroCnh",
                new
                {
                    Cnpj = cnpj,
                    NumeroCnh = numeroCnh
                });

            return result;

        }


        public async Task<Entregador> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstOrDefaultAsync<Entregador>(@"
                                                SELECT 
                                                       id
                                                      ,cnpj
                                                      ,numerocnh
                                                      ,tipocnh
                                                      ,imagemid
                                                    FROM Entregador
                                                      Where id = @Id",
                                                     
                new
                {
                    Id = id
                });

            return result;

        }
    }
}
