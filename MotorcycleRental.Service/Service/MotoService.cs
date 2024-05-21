using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Minio.DataModel;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;
using MotorcycleRental.Domain.Validators;

namespace MotorcycleRental.Service.Service
{
    public class MotoService : IMotoService
    {
        private readonly IMapper _mapper;
        private readonly IMotoRepository _motoRepository;
        private readonly ILogger<MotoService> _logger;
        private readonly ILocacaoService _locacaoService;
        public MotoService(IMotoRepository motoRepository, IMapper mapper, ILogger<MotoService> logger, ILocacaoService locacaoService = null)
        {
            _motoRepository = motoRepository;
            _mapper = mapper;
            _logger = logger;
            _locacaoService = locacaoService;
        }

        public async Task<List<MotoResponseModel>> GetAllAsync()
        {
            try
            {
                var motocycle = await _motoRepository.GetAllAsync();
                var response = _mapper.Map<List<MotoResponseModel>>(motocycle);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[MotoService - GetAllAsync] - Não foi possivel buscar os registros : {ex.Message}");
                throw;
            }
        }

        public async Task<MotoResponseModel> GetByIdAsync(int id)
        {
            try
            {
                var motocycle = await _motoRepository.GetByIdAsync(id);
                var response = _mapper.Map<MotoResponseModel>(motocycle);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[MotoService - GetByIdAsync] - Não foi possivel buscar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<MotoResponseModel> AddAsync(MotoRequestModel request)
        {
            var response = new MotoResponseModel();

            try
            {
                if (await ValidaEntidadeAsync(request))
                {
                    var motocycle = await _motoRepository.GetByPlacaAsync(request.Placa.Trim());
                    if (motocycle != null)
                    {
                        response.AddErrorValidation(StatusCodes.Status422UnprocessableEntity, $"Já existe uma moto com esse numero de placa : {motocycle.Placa}");
                        return response;
                    }

                    var motocycleRequest = _mapper.Map<Moto>(request);
                    var motocycleResponse = await _motoRepository.AddAsync(motocycleRequest);

                    response = _mapper.Map<MotoResponseModel>(motocycleResponse);
                }
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[MotoService - AddAsync] - Não foi possivel adicionar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<MotoResponseModel> GetPlacaMoto(string placa)
        {
            try
            {
                var motocycle = await _motoRepository.GetByPlacaAsync(placa.Trim());

                var response = _mapper.Map<MotoResponseModel>(motocycle);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[MotoService - GetPlacaMoto] - Não foi possivel buscar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<MotoResponseModel> UpdatePlacaAsync(int id, string placa)
        {
            var response = new MotoResponseModel();

            try
            {

                var motocycle = await _motoRepository.GetByIdAsync(id);
                if (motocycle == null)
                {
                    response.AddErrorValidation(StatusCodes.Status422UnprocessableEntity, $"Não existe o registro dessa moto id : {id}");
                    return response;
                }

                var motocycleResponse = await _motoRepository.UpdatePlacaAsync(id, placa.Trim());

                response = _mapper.Map<MotoResponseModel>(motocycleResponse);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[MotoService - UpdatePlacaAsync] - Não foi possivel atualizar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<MotoResponseModel> DeleteAsync(int id)
        {
            var responseModel = new MotoResponseModel();

            try
            {
                var locacoes = await _locacaoService.GetByMotoIdAsync(id);
                if (locacoes.Any(item => item.Status == "AT"))
                {
                    responseModel.AddErrorValidation(StatusCodes.Status404NotFound, $"Não foi possivel remover essa moto, ela esta em registro de locação");
                    return responseModel;
                }

                var motocycleResponse = await _motoRepository.DeleteAsync(id);

                return responseModel;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[MotoService - DeleteAsync] - Não foi possivel remover o registro : {ex.Message}");
                throw;
            }
        }

        private async Task<bool> ValidaEntidadeAsync(MotoRequestModel motocycleRequestModel)
        {
            var validator = new MotoValidator();
            var validatorResult = await validator.ValidateAsync(motocycleRequestModel);

            foreach (var validation in validatorResult.Errors)
            {
                throw new ArgumentException($"Entidade inválida - {validation.ErrorMessage}");
            }

            return validatorResult.IsValid;
        }
    }
}
