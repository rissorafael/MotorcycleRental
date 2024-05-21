using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class MotoController : ControllerBase
    {
        private IMotoService _motoService;

        public MotoController(IMotoService motoService)
        {
            _motoService = motoService;
        }


        /// <summary>
        /// Consulta todas as motos que estão cadastradas.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Retorna todas as motos</response>
        ///  <response code ="204">Nenhuma moto foi encontrada</response>
        [Authorize(Roles = "Admin, Entregador")]
        [HttpGet("all")]
        [ProducesResponseType(typeof(MotoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetAllMoto()
        {

            var response = await _motoService.GetAllAsync();

            if (response == null)
                return NoContent();

            return Ok(response);

        }

        /// <summary>
        /// Realiza a consulta os dados da moto pela placa.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Retorna os dados da moto pela placa</response>
        ///  <response code ="204">Nenhuma moto foi encontrada</response>
        [Authorize(Roles = "Admin")]
        [HttpGet("{placa}")]
        [ProducesResponseType(typeof(MotoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetPlacaMoto(string placa)
        {
            var response = await _motoService.GetPlacaMoto(placa);

            if (response == null)
                return NoContent();

            return Ok(response);

        }

        /// <summary>
        /// Adiciona uma nova moto.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Incluir uma nova moto</response>
        [Authorize(Roles = "Admin")]
        [HttpPost("moto")]
        [ProducesResponseType(typeof(MotoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddMoto([FromBody] MotoRequestModel request)
        {
            var response = await _motoService.AddAsync(request);

            if (!response.IsValid)
                return StatusCode(response.StatusCode, response.Description);

            return Ok(response);
        }


        /// <summary>
        /// Atualizar a placa da moto.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Atualizar a placa da moto</response>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}/{placa}")]
        [ProducesResponseType(typeof(MotoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> updateMoto(int id, string placa)
        {
            var response = await _motoService.UpdatePlacaAsync(id, placa);

            if (!response.IsValid)
                return StatusCode(response.StatusCode, response.Description);

            return Ok(response);
        }

        /// <summary>
        /// Remover um cadastro de uma moto.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Remover um cadastro de uma moto</response>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MotoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> deleteMoto(int id)
        {
            var response = await _motoService.DeleteAsync(id);

            if (!response.IsValid)
                return StatusCode(response.StatusCode, response.Description);

            return Ok();
        }
    }
}
