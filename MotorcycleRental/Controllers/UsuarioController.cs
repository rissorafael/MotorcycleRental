using Microsoft.AspNetCore.Mvc;
using MotorcycleRental.Domain.Interfaces;
using MotorcycleRental.Domain.Models;

namespace MotorcycleRental.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Cadastra um novo usuario.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Incluir novo usuario</response>
        [HttpPost("cadastro")]
        [ProducesResponseType(typeof(UsuarioResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> AddUsuarioAsync([FromForm] UsuarioRequestModel request)
        {
            var response = await _usuarioService.AddAsync(request);

            if (!response.IsValid)
                return StatusCode(response.StatusCode, response.Description);

            return Ok(response);
        }


        /// <summary>
        /// Gera um novo Token.
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Cria um token para acessar os endpoints permitidos </response>
        [HttpPost("autenticacao")]
        [ProducesResponseType(typeof(AutenticacaoResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Autenticacao([FromForm] AutenticacaoRequestModel request)
        {
            var response = await _usuarioService.LoginAsync(request);

            if (!response.IsValid)
                return StatusCode(response.StatusCode, response.Description);

            return Ok(response);
        }

    }
}
