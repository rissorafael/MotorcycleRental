using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Domain.Entities;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Service.Service
{
    public class ContratoService : IContratoService
    {
        private readonly IMapper _mapper;
        private readonly IContratoRepository _contratoRepository;
        private readonly ILogger<ContratoService> _logger;
        private readonly ILocacaoService _locacaoService;
        private readonly IPlanosService _planosService;
        private readonly IProducer _producer;

        public ContratoService(IMapper mapper,
            IContratoRepository contratoRepository,
            ILogger<ContratoService> logger,
            ILocacaoService locacaoService,
            IPlanosService planosService,
            IProducer producer)
        {
            _mapper = mapper;
            _contratoRepository = contratoRepository;
            _logger = logger;
            _locacaoService = locacaoService;
            _planosService = planosService;
            _producer = producer;
        }


        public async Task<ContratoResponseModel> GetByIdLocacaoAsync(int idLocacao)
        {
            try
            {
                var contrato = await _contratoRepository.GetByIdLocacaoAsync(idLocacao);
                var response = _mapper.Map<ContratoResponseModel>(contrato);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ContratoService - GetByIdLocacaoAsync] - Não foi possivel buscar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<ContratoResponseModel> AddAsync(int idLocacao, DateTime dataFim)
        {
            var response = new ContratoResponseModel();
            var request = new ContratoRequestModel();

            try
            {
                var contrato = await GetByIdLocacaoAsync(idLocacao);
                if (contrato != null)
                {
                    response.AddErrorValidation(StatusCodes.Status404NotFound, $"Este contrato já foi efetivado.");
                    return response;
                }


                var locacao = await _locacaoService.GetByIdAsync(idLocacao);
                if (locacao == null)
                {
                    response.AddErrorValidation(StatusCodes.Status404NotFound, $"Não existe locacao com Id");
                    return response;
                }

                var planos = await _planosService.GetByIdAsync(locacao.PlanosId);

                var calculoResponse = await _planosService.GetCauculoPlanosAsync(planos.QuantidadeDias, locacao.DataInicio, dataFim);

                request.IdLocacao = locacao.Id;
                request.IdPlano = locacao.PlanosId;
                request.IdEntregador = locacao.Entregadorid;
                request.DataInicio = locacao.DataInicio;
                request.ValorTotalLocacao = Convert.ToDecimal(calculoResponse.ValorTotal);
                request.DiasEfetivados = calculoResponse.QuantidadesDias;
                request.DataFim = dataFim;

                var contratoRequest = _mapper.Map<Contrato>(request);
                var contratoResponse = await _contratoRepository.AddAsync(contratoRequest);

                await _locacaoService.UpdateStatusAsync(idLocacao, "IN");

                response = _mapper.Map<ContratoResponseModel>(contratoResponse);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ContratoService - AddAsync] - Não foi possivel adicionar o registro : {ex.Message}");
                throw;
            }
        }

        public void ContratoProducer(int locacaoId, DateTime dataFim)
        {
            try
            {
                var request = new TransacaoMensagem{
                    IdLocacao = locacaoId,
                    dataFim = dataFim
                };

                _producer.PublicaMenssagem(request);
            }
            catch (Exception ex)
            {
                _logger.LogError($"[ContratoService - LocacaoMoto] - Não foi possivel publicar _producer.PublicaMenssagem Error: {ex.Message}");
                throw;
            }
        }
    }
}
