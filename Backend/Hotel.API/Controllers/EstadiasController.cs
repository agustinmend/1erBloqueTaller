using Microsoft.AspNetCore.Mvc;
using Hotel.Models.DTOs;
using Hotel.Services.Interfaces;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadiasController : ControllerBase
    {
        private readonly IEstadiaService _estadiaService;
        public EstadiasController(IEstadiaService estadiaService)
        {
            _estadiaService = estadiaService;
        }

        [HttpPost("checkin")]
        public async Task<IActionResult> RegistrarCheckIn([FromBody] CheckInDto dto)
        {
            try
            {
                int nuevaEstadiaId = await _estadiaService.ProcesarCheckInAsync(dto);
                return Ok(new { mensaje = "Checkin reallizado exitosamente", estadiaId = nuevaEstadiaId });
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch(Exception)
            {
                return StatusCode(500, new { error = "Error interno al registrar check-in" });
            }
        }

        [HttpGet("pre-checkin/{reservaId}")]
        public async Task<IActionResult> ObtenerPreCheckIn(int reservaId)
        {
            try
            {
                var datos = await _estadiaService.ObtenerDatosPreCheckInAsync(reservaId);
                return Ok(datos);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno al cargar datos del check-in.", detalleExacto = ex.Message, origen = ex.Source });
            }
        }
    }
}
