using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Enums;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;
using MotorcycleRental.Domain.Validators;
using MotorcycleRental.Infra.CrossCutting.ExtensionMethods;


namespace MotorcycleRental.Service.Service
{
    public class EntregadorService : IEntregadorService
    {

        private readonly IMapper _mapper;
        private readonly IEntregadorRepository _entregadorRepository;
        private readonly ILogger<EntregadorService> _logger;
        private readonly IUsuarioService _usuarioService;
        private readonly IRolesService _rolesService;

        public EntregadorService(IMapper mapper,
                                IEntregadorRepository entregadorRepository,
                                ILogger<EntregadorService> logger,
                                IUsuarioService usuarioService,
                                IRolesService rolesService)
        {
            _mapper = mapper;
            _entregadorRepository = entregadorRepository;
            _logger = logger;
            _usuarioService = usuarioService;
            _rolesService = rolesService;
        }


        public async Task<EntregadorResponseModel> GetByIdAsync(int id)
        {
            try
            {
                var entregador = await _entregadorRepository.GetByIdAsync(id);
                var response = _mapper.Map<EntregadorResponseModel>(entregador);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[EntregadorService - GetByIdAsync] - Não foi possivel buscar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<EntregadorResponseModel> AddAsync(EntregadorRequestModel request)
        {
            var response = new EntregadorResponseModel();

            try
            {
                if (await ValidaEntidadeAsync(request))
                {

                    var fileExtension = Path.GetExtension(request.ImagemDocumento.FileName);
                    if (fileExtension != FormatoDocumentos.PNG && fileExtension != FormatoDocumentos.BMP)
                    {
                        response.AddErrorValidation(StatusCodes.Status400BadRequest, $"O Formato da imagem deve ser png ou bmp");
                        return response;
                    }

                    var usuario = await _usuarioService.GetByUserNameAsync(request.UserName);
                    if (usuario == null)
                    {
                        response.AddErrorValidation(StatusCodes.Status404NotFound, $"Usuario não encontrado : {request.UserName}");
                        return response;
                    }

                    var imagemId = SaveImagemDocument(request.ImagemDocumento);

                    request.Cnpj = request.Cnpj.Replace("-", "").Replace("/", "").Replace(".", "");

                    var entregador = await _entregadorRepository.GetByCnpjOrNumeroCnhAsync(request.Cnpj, request.NumeroCnh);
                    if (entregador != null)
                    {
                        response.AddErrorValidation(StatusCodes.Status400BadRequest, $"Já existe um usario com esse numero de registro");
                        return response;
                    }

                    var entregadorequest = _mapper.Map<Entregador>(request);

                    entregadorequest.ImagemId = imagemId;
                    entregadorequest.TipoCnh = string.Join(",", request.TipoCnh);
                    entregadorequest.UsuarioId = usuario.Id;

                    var entregadorResponse = await _entregadorRepository.AddAsync(entregadorequest);

                    var roles = new RolesRequestModel()
                    {
                        Role = "Entregador",
                        UsuarioId = usuario.Id,
                        Descricao = "UserPolicy Entregador"
                    };

                    await _rolesService.AddAsync(roles);

                    response = _mapper.Map<EntregadorResponseModel>(entregadorResponse);
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[entregadorService - AddAsync] - Não foi possivel adicionar o registro : {ex.Message}");
                throw;
            }
        }

        private async Task<bool> ValidaEntidadeAsync(EntregadorRequestModel entregadorRequestModel)
        {
            var validator = new EntregadorValidator();
            var validatorResult = await validator.ValidateAsync(entregadorRequestModel);

            foreach (var validation in validatorResult.Errors)
            {
                throw new ArgumentException($"Entidade inválida - {validation.ErrorMessage}");
            }

            return validatorResult.IsValid;
        }

        public string SaveImagemDocument(IFormFile ImagemDocumento)
        {
            string directoryPath = ExatractConfiguration.GetImagemPath;

            string guid = Guid.NewGuid().ToString();

            string newName = $"{guid}.jpeg";

            string destinoPath = Path.Combine(directoryPath, newName);

            using (var strem = new FileStream(destinoPath, FileMode.Create, FileAccess.Write))
            {
                ImagemDocumento.CopyTo(strem);
            }
            return guid;
        }
    }
}
