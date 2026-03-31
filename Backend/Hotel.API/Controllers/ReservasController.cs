using Microsoft.AspNetCore.Mvc;
using Hotel.Models.DTOs;
using Hotel.Services.Interfaces;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaService _reservaService;
        public ReservasController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearReserva([FromBody] CrearReservaDto dto)
        {
            try
            {
                int nuevaReservaId = await _reservaService.CrearReservaAsync(dto);
                return StatusCode(201, new
                {
                    mensaje = "Reserva creada exitosamente",
                    reservaId = nuevaReservaId
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch(Exception)
            {
                return StatusCode(500, new { error = "Ocurrio un error interno al hacer la reserva" });
            }
        }
        [HttpGet("agenda")]
        public async Task<IActionResult> ObtenerAgenda([FromQuery] string? buscar = null)
        {
            try
            {
                var agenda = await _reservaService.ObtenerAgendaAsync(buscar);
                return Ok(agenda);
            }
            catch(Exception)
            {
                return StatusCode(500, new { error = "Ocurrio un error al cargar las reservas" });
            }
        }
    }
}