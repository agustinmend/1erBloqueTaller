using Microsoft.AspNetCore.Mvc;
using Hotel.Services.Interfaces;
using Hotel.Models.DTOs;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HuespedController : ControllerBase
    {
        private readonly IHuespedService _huespedService;
        public HuespedController(IHuespedService huespedService)
        {
            _huespedService = huespedService;
        }
        [HttpPost]
        public async Task<IActionResult> RegistrarHuesped([FromBody] CrearHuespedDto dto)
        {
            try
            {
                int nuevoId = await _huespedService.RegistrarHuespedAsync(dto);
                return StatusCode(201, new { mensaje = "Huesped registrado corrrectamente", id = nuevoId });
            }
            catch(InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch(Exception)
            {
                return StatusCode(500, new { error = "Ocurrio un error interno del servidor" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _huespedService.ObtenerTodosAsync();
            return Ok(lista);
        }
    }
}
