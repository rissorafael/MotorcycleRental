using AutoMapper;
using Microsoft.Extensions.Logging;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Service.Service
{
    public class PlanosService : IPlanosService
    {
        private readonly IMapper _mapper;
        private readonly IPlanosRepository _planosRepository;
        private readonly ILogger<PlanosService> _logger;


        public PlanosService(IMapper mapper, IPlanosRepository planosRepository, ILogger<PlanosService> logger)
        {
            _mapper = mapper;
            _planosRepository = planosRepository;
            _logger = logger;
        }

        public async Task<PlanosResponseModel> GetByIdAsync(int id)
        {
            try
            {
                var plano = await _planosRepository.GetByIdAsync(id);
                var response = _mapper.Map<PlanosResponseModel>(plano);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[PlanosService - GetByIdAsync] - Não foi possivel buscar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<List<PlanosResponseModel>> GetAllAsync()
        {
            try
            {
                var planos = await _planosRepository.GetAllAsync();
                var response = _mapper.Map<List<PlanosResponseModel>>(planos);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[PlanosService - GetAllAsync] - Não foi possivel buscar o registro : {ex.Message}");
                throw;
            }
        }

        public async Task<(double ValorTotal, int QuantidadesDias)> GetCauculoPlanosAsync(int planoQtdDias, DateTime dataInicio, DateTime? dataFim)
        {
            DateTime dataEntrega;

            var planos = await _planosRepository.GetByPlanoQuantidadeDiasAsync(planoQtdDias);

            if (dataFim == null)
            {
                dataEntrega = new DateTime(dataInicio.Year, dataInicio.Month, dataInicio.AddDays(planos.QuantidadeDias).Day);
            }
            else
            {
                dataEntrega = new DateTime(dataFim.Value.Year, dataFim.Value.Month, dataFim.Value.Day);
            }

            var dataRetirada = new DateTime(dataInicio.Year, dataInicio.Month, dataInicio.Day);

            TimeSpan diferenca = dataEntrega - dataRetirada;
            int numeroDeDias = diferenca.Days;


            var valor = await GetCauculoAsync(numeroDeDias, planoQtdDias, Convert.ToDouble(planos.ValorDiaria), planos.JurosDiariasNEfetivadas);

            return (valor, numeroDeDias);
        }


        private async Task<double> GetCauculoAsync(int numeroDeDias, int planoQtdDias, double custoDiario, int juros)
        {
            try
            {
                double valorTotal = 0;

                if (numeroDeDias < planoQtdDias)
                {
                    int diasMulta = planoQtdDias - numeroDeDias;

                    double ValorDiariasEfetivadas = numeroDeDias * custoDiario;
                    double ValorDiariasNaoEfetivadas = diasMulta * custoDiario;

                    var diasJuros = ValorDiariasNaoEfetivadas * juros;

                    var valorDiasMultas = diasJuros / 100;

                    valorTotal = valorDiasMultas + ValorDiariasEfetivadas;

                    return valorTotal;
                }

                if (numeroDeDias == planoQtdDias)
                {
                    double ValorDiariasEfetivadas = numeroDeDias * custoDiario;
                    valorTotal = ValorDiariasEfetivadas;

                    return valorTotal;
                }


                if (numeroDeDias > planoQtdDias)
                {
                    int diasUltrapassados = numeroDeDias - planoQtdDias;

                    double ValorDiariasSuperior = diasUltrapassados * 50;
                    double ValorDiariasEfetivadas = planoQtdDias * custoDiario;

                    valorTotal = ValorDiariasSuperior + ValorDiariasEfetivadas;

                    return valorTotal;
                }

                return valorTotal;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[PlanosService - GetCauculoAsync] - Não foi possivel buscar o registro : {ex.Message}");
                throw;
            }
        }
    }
}
