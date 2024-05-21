using Dapper;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;

namespace MotorcycleRental.Infra.Data.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public RolesRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Roles>> GetByUsuarioIdAsync(int usuarioId)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryAsync<Roles>(@"
                                                SELECT 
                                                       id
                                                      ,role
                                                      ,usuarioid
                                                      ,descricao
                                                  FROM Roles
                                                      Where usuarioId =@UsuarioId",
                new
                {
                    UsuarioId = usuarioId
                });

            return result;
        }

        public async Task<Roles> AddAsync(Roles roles)
        {

            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstAsync<Roles>(@"
                              
                              INSERT INTO Roles
                              (
                                 role
	                            ,usuarioid  
	                            ,descricao
	                          )
                             VALUES 
                             (
                                 @role
	                            ,@usuarioid  
	                            ,@descricao
                              )
                           RETURNING 
                               	id
	                           ,role
	                           ,usuarioid
	                           ,descricao
	                         ;",
                new
                {
                    roles.Role,
                    roles.UsuarioId,
                    roles.Descricao
                });

            return result;
        }
    }
}
