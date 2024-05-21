using Dapper;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;


namespace MotorcycleRental.Infra.Data.Repositories
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public LocacaoRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Locacao> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstOrDefaultAsync<Locacao>(@"
                                                SELECT 
                                                      id
	                                                 ,motoid
	                                                 ,entregadorid
	                                                 ,datainicio
	                                                 ,dataprevistofim
                                                     ,datacriacao
	                                                 ,planosid
	                                                 ,status
	                                                 ,diasprevisto
	                                                 ,valorlocacao
                                                   FROM Locacao
                                                      Where id = @Id",
                new
                {
                    Id = id
                });

            return result;
        }

        public async Task<Locacao> AddAsync(Locacao locacao)
        {

            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstAsync<Locacao>(@"
                              
                              INSERT INTO Locacao
                              (
                                 motoid
	                            ,entregadorid
	                            ,datainicio
	                            ,dataprevistofim
                                ,datacriacao
	                            ,planosid
	                            ,status
	                            ,diasprevisto
	                            ,valorlocacao
                              )
                             VALUES 
                             (
                                @motoid
	                           ,@entregadorid
	                           ,@datainicio
	                           ,@dataprevistofim
                               ,@datacriacao
	                           ,@planosid
	                           ,@status
	                           ,@diasprevisto
	                           ,@valorlocacao
                              )
                           RETURNING 
                               	id
	                           ,motoid
	                           ,entregadorid
	                           ,datainicio
	                           ,dataprevistofim
                               ,datacriacao
	                           ,planosid
	                           ,status
	                           ,diasprevisto
	                           ,valorlocacao
                             ;",
                new
                {
                    locacao.MotoId,
                    locacao.EntregadorId,
                    locacao.DataInicio,
                    locacao.DataPrevistoFim,
                    locacao.DataCriacao,
                    locacao.PlanosId,
                    locacao.Status,
                    locacao.DiasPrevisto,
                    locacao.ValorLocacao,
                });

            return result;

        }

        public async Task<IEnumerable<Locacao>> GetByMotoIdAsync(int motoId)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryAsync<Locacao>(@"
                                                SELECT 
                                                      id
	                                                 ,motoId
	                                                 ,entregadorid
	                                                 ,datainicio
	                                                 ,dataprevistofim
                                                     ,datacriacao
	                                                 ,planosid
	                                                 ,status
	                                                 ,diasprevisto
	                                                 ,valorlocacao
                                                   FROM Locacao
                                                      Where motoid = @MotoId",
                new
                {
                    MotoId = motoId
                });

            return result;
        }

        public async Task<Locacao> UpdateStatusAsync(int id, string status)
        {

            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstAsync<Locacao>(@"
                              
                              UPDATE Locacao
                              SET status = @Status
                                WHERE id = @Id
                           RETURNING 
                               id 
	                           ,motoid
	                           ,entregadorid 
	                           ,datainicio
	                           ,dataprevistofim
                               ,datacriacao
	                           ,planosid
	                           ,status
	                           ,diasprevisto
	                           ,valorlocacao
                             ;",
                new
                {
                    Status = status,
                    Id = id
                });

            return result;
        }
    }
}
