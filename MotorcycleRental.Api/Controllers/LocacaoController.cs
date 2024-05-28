using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class LocacaoController : ControllerBase
    {

        private IPlanosService _planosService;
        private IContratoService _contratoService;
        private ILocacaoService _locacaoService;

        public LocacaoController(IPlanosService planosService,
                                IContratoService contratoService,
                                ILocacaoService locacaoService)
        {
            _planosService = planosService;
            _contratoService = contratoService;
            _locacaoService = locacaoService;
        }


        /// <summary>
        /// Consultar valor dos planos.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Retorna o calculo dos planos</response>
        ///  <response code ="204">Nenhuma moto foi encontrada</response>
        [Authorize(Roles = "Entregador")]
        [HttpGet("simulacaoPlanos/{planoQtdDias}/{dataRetirada}")]
        [ProducesResponseType(typeof((double, int)), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SimulacaoPlanosAsync(int planoQtdDias, DateTime dataRetirada, DateTime? dataEntrega)
        {

            (double ValorTotal, int QuantidadesDias) = await _planosService.GetCauculoPlanosAsync(planoQtdDias, dataRetirada, dataEntrega);

            if (ValorTotal < 1 && QuantidadesDias < 1)
                return NoContent();

            return Ok($"ValorTotal : {ValorTotal} QuantidadesDias : {QuantidadesDias}");

        }


        /// <summary>
        /// Realiza uma locacao.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Incluir uma nova locacao</response>
        [Authorize(Roles = "Entregador")]
        [HttpPost("locacao")]
        [ProducesResponseType(typeof(LocacaoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddAsync([FromBody] LocacaoRequestModel request)
        {

            var response = await _locacaoService.AddAsync(request);
            if (!response.IsValid)
                return StatusCode(response.StatusCode, response.Description);

            return Ok(response);

        }

        /// <summary>
        /// Realiza um contrato.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Incluir um novo contrato</response>
        [Authorize(Roles = "Admin, Entregador")]
        [HttpPost("contrato/{locacaoId}/{dataFim}")]
        [ProducesResponseType(typeof(ContratoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddContratoAsync(int locacaoId, DateTime dataFim)
        {
            try
            {
                _contratoService.ContratoProducer(locacaoId, dataFim);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>
        /// Consultar todos os planos.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Retorna os dados dos planos</response>
        ///  <response code ="204">Nenhum plano foi encontrado</response>
        [HttpGet("planos")]
        [ProducesResponseType(typeof(PlanosResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllPlanosAsync()
        {
            var response = await _planosService.GetAllAsync();

            if (response == null)
                return NoContent();

            return Ok(response);

        }

    }
}

