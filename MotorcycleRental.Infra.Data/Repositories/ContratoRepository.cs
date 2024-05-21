using Dapper;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;

namespace MotorcycleRental.Infra.Data.Repositories
{
    public class ContratoRepository : IContratoRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ContratoRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Contrato> GetByIdLocacaoAsync(int idLocacao)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstOrDefaultAsync<Contrato>(@"
                                     SELECT 
                                         id
	                                    ,identregador
	                                    ,idplano
	                                    ,datainicio
	                                    ,datafim
	                                    ,diasefetivados
	                                    ,valortotallocacao
	                                    ,idlocacao
                                       FROM Contrato
                                           Where idLocacao = @idLocacao",
                new
                {
                    idLocacao
                });

            return result;

        }


        public async Task<Contrato> AddAsync(Contrato contrato)
        {

            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstAsync<Contrato>(@"
                              
                              INSERT INTO Contrato
                              (
                                  identregador
	                             ,idplano
	                             ,datainicio
	                             ,datafim
	                             ,diasefetivados
	                             ,valortotallocacao
	                             ,idlocacao
                              )
                             VALUES 
                             (
                                  @identregador
	                             ,@idplano
	                             ,@datainicio
	                             ,@datafim
	                             ,@diasefetivados
	                             ,@valortotallocacao
	                             ,@idlocacao
                              )
                           RETURNING 
                                 id
                                ,identregador
	                            ,idplano
	                            ,datainicio
	                            ,datafim
	                            ,diasefetivados
	                            ,valortotallocacao
	                            ,idlocacao
                             ;",
                new
                {
                    contrato.IdEntregador,
                    contrato.IdPlano,
                    contrato.DataInicio,
                    contrato.DataFim,
                    contrato.DiasEfetivados,
                    contrato.ValorTotalLocacao,
                    contrato.IdLocacao,
                });

            return result;

        }
    }
}
