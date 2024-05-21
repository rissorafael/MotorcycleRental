using Dapper;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;

namespace MotorcycleRental.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public UsuarioRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }


        public async Task<Usuario> GetByUserNameAsync(string userName)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstOrDefaultAsync<Usuario>(@"
                                                SELECT 
                                                     id
                                                     ,nome
                                                     ,datanascimento
                                                     ,senha
                                                     ,username
                                                     ,email
                                                     ,telefone
                                                  FROM usuario
                                                      Where username =@userName",
                new
                {
                    userName = userName
                });

            return result;
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            using var connection = _connectionFactory.Connection();
            var result = await connection.QueryFirstAsync<Usuario>(@"
                              
                              INSERT INTO  Usuario
                              (
                                 Nome
                                ,DataNascimento
                                ,Senha 
                                ,UserName
                                ,Email
                                ,Telefone
                              )
                             VALUES 
                             (
                                 @Nome
                                ,@DataNascimento
                                ,@Senha 
                                ,@UserName
                                ,@Email
                                ,@Telefone
                              )
                           RETURNING 
                                 Id                                
                                ,Nome
                                ,DataNascimento
                                ,Senha 
                                ,UserName
                                ,Email
                                ,Telefone
                             ;",
                new
                {
                    usuario.Id,
                    usuario.Nome,
                    usuario.DataNascimento   ,
                    usuario.Senha,
                    usuario.UserName,
                    usuario.Email,
                    usuario.Telefone
                });

            return result;

        }
    }
}
