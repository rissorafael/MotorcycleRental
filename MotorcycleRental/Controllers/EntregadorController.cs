using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class EntregadorController : ControllerBase
    {
        private IEntregadorService _entregadorService;
        public EntregadorController(IEntregadorService entregadorService)
        {
            _entregadorService = entregadorService;
        }

        /// <summary>
        /// Adiciona um novo entregador.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Incluir novo entregador</response>
        [HttpPost("entregador")]
        [ProducesResponseType(typeof(EntregadorResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddUser([FromForm] EntregadorRequestModel request)
        {
            var response = await _entregadorService.AddAsync(request);

            if (!response.IsValid)
                return StatusCode(response.StatusCode, response.Description);

            return Ok(response);
        }
    }
}
