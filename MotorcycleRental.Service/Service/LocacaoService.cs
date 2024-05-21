using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Service.Service
{
    public class LocacaoService : ILocacaoService
    {

        private readonly IMapper _mapper;
        private readonly ILocacaoRepository _locacaoRepository;
        private readonly ILogger<LocacaoService> _logger;
        private readonly IEntregadorService _entregadorService;
        private readonly IPlanosService _planosService;

        public LocacaoService(IMapper mapper, ILocacaoRepository locacaoRepository, ILogger<LocacaoService> logger, IEntregadorService entregadorService, IPlanosService planosService = null)
        {
            _mapper = mapper;
            _locacaoRepository = locacaoRepository;
            _logger = logger;
            _entregadorService = entregadorService;
            _planosService = planosService;
        }

        public async Task<LocacaoResponseModel> GetByIdAsync(int id)
        {
            try
            {
                var locacao = await _locacaoRepository.GetByIdAsync(id);
                var response = _mapper.Map<LocacaoResponseModel>(locacao);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[LocacaoService - GetByIdAsync] - Não foi possivel buscar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<LocacaoResponseModel> UpdateStatusAsync(int id, string status)
        {
            try
            {
                var locacao = await _locacaoRepository.UpdateStatusAsync(id, status);
                var response = _mapper.Map<LocacaoResponseModel>(locacao);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[LocacaoService - GetByIdAsync] - Não foi possivel buscar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<List<LocacaoResponseModel>> GetByMotoIdAsync(int motoId)
        {
            try
            {
                var locacoes = await _locacaoRepository.GetByMotoIdAsync(motoId);
                var response = _mapper.Map<List<LocacaoResponseModel>>(locacoes);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[LocacaoService - GetByMotoIdAsync] - Não foi possivel buscar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<LocacaoResponseModel> AddAsync(LocacaoRequestModel request)
        {
            try
            {
                var response = new LocacaoResponseModel();

                var locacoes = await _locacaoRepository.GetByMotoIdAsync(request.MotoId);

                if (locacoes.Any(item => item.Status == "AT"))
                {
                    response.AddErrorValidation(StatusCodes.Status404NotFound, $"Essa moto ja esta alugada Id : {request.MotoId}");
                    return response;
                }

                var entregador = await _entregadorService.GetByIdAsync(request.Entregadorid);
                if (entregador == null)
                {
                    response.AddErrorValidation(StatusCodes.Status404NotFound, $"Não existe entregador com esse Id : {request.Entregadorid}");
                    return response;
                }

                List<string> list = entregador.TipoCnh.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (!list.Any(x => x == "A"))
                {
                    response.AddErrorValidation(StatusCodes.Status404NotFound, $"Usuario não possui categora A de habilitação");
                    return response;
                }

                var plano = await _planosService.GetByIdAsync(request.PlanosId);
                if (plano == null)
                {
                    response.AddErrorValidation(StatusCodes.Status404NotFound, $"Não existe plano com esse Id : {request.PlanosId}");
                    return response;
                }

                var valorlocacao = await _planosService.GetCauculoPlanosAsync(plano.QuantidadeDias, request.DataInicio, request.DataPrevistoFim);

                var requestLocacao = _mapper.Map<Locacao>(request);

                requestLocacao.ValorLocacao = Convert.ToDecimal(valorlocacao.ValorTotal);
                requestLocacao.Status = "AT";
                requestLocacao.DiasPrevisto = valorlocacao.QuantidadesDias;
                requestLocacao.DataCriacao = DateTime.Now;

                var locacao = await _locacaoRepository.AddAsync(_mapper.Map<Locacao>(requestLocacao));

                response = _mapper.Map<LocacaoResponseModel>(locacao);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[LocacaoService - AddAsync] - Não foi possivel adicionar o registro : {ex.Message}");
                throw;
            }
        }
    }
}
