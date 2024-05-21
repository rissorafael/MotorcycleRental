using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;
using MotorcycleRental.Domain.Validators;
using System.Security.Claims;


namespace MotorcycleRental.Service.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILogger<UsuarioService> _logger;
        private readonly IRolesService _rolesService;
        private readonly ITokenService _tokenService;

        public UsuarioService(IMapper mapper, IUsuarioRepository usuarioRepository, ILogger<UsuarioService> logger, IRolesService rolesService, ITokenService tokenService)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
            _rolesService = rolesService;
            _tokenService = tokenService;
        }


        public async Task<UsuarioResponseModel> GetByUserNameAsync(string userName)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByUserNameAsync(userName);
                var response = _mapper.Map<UsuarioResponseModel>(usuario);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[UsuarioService - GetByUserNameAsync] - Não foi possivel buscar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<UsuarioResponseModel> AddAsync(UsuarioRequestModel request)
        {
            var response = new UsuarioResponseModel();

            try
            {
                if (await ValidaEntidadeAsync(request))
                {
                    var userName = await _usuarioRepository.GetByUserNameAsync(request.UserName);
                    if (userName != null)
                    {
                        response.AddErrorValidation(StatusCodes.Status422UnprocessableEntity, $"Já existe um usuario com esse UserName : {request.UserName}");
                        return response;
                    }

                    request.Telefone = request.Telefone.Replace("-", "").Replace("(", "").Replace(")", "");

                    request.Senha = Criptografia.Encrypt(request.Senha);
                    Usuario usuario = _mapper.Map<Usuario>(request);

                    var user = await _usuarioRepository.AddAsync(usuario);

                    response = _mapper.Map<UsuarioResponseModel>(user);
                }

                return response;
            }

            catch (Exception ex)
            {
                _logger.LogError($"[UsuarioService - AddAsync] - Falha ao cadastrar usuário : {ex.Message}");
                throw;
            }
        }


        public async Task<AutenticacaoResponseModel> LoginAsync(AutenticacaoRequestModel request)
        {

            var response = new AutenticacaoResponseModel();
            try
            {
                var user = await _usuarioRepository.GetByUserNameAsync(request.UserName);
                if (user == null)
                {
                    response.AddErrorValidation(StatusCodes.Status404NotFound, $"Usuario não encontrado : {request.UserName}");
                    return response;
                }

                var senha = Criptografia.Decrypt(user.Senha);
                if (!StringComparer.Ordinal.Equals(request.Senha, senha))
                {
                    response.AddErrorValidation(StatusCodes.Status401Unauthorized, "Senha incorreta!");
                    return response;
                }

                var listRoles = await _rolesService.GetByUsuarioIdAsync(user.Id);
                if (listRoles == null)
                {
                    response.AddErrorValidation(StatusCodes.Status401Unauthorized, "Sem Autorizacao para gerar Token!");
                    return response;
                }

                var claims = new List<Claim>
                  {
                   new Claim(ClaimTypes.Name, user.UserName),
                   new Claim(ClaimTypes.Email, user.Email)
                  };


                foreach (var role in listRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Role));
                }

                var token = _tokenService.GenerateToken(claims);
                if (token != null) response.Token = token;

                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"[UsuarioService - LoginAsync] - Não foi possivel gerar o token : {ex.Message}");
                throw;
            }
        }


        private async Task<bool> ValidaEntidadeAsync(UsuarioRequestModel usuarioRequestModel)
        {
            var validator = new UsuarioValidator();
            var validatorResult = await validator.ValidateAsync(usuarioRequestModel);

            foreach (var validation in validatorResult.Errors)
            {
                throw new ArgumentException($"Entidade inválida - {validation.ErrorMessage}");
            }

            return validatorResult.IsValid;
        }
    }
}
