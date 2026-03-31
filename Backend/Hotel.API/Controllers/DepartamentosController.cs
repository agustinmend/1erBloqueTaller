using Microsoft.AspNetCore.Mvc;
using Hotel.Services.Interfaces;

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;
        public DepartamentosController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        [HttpGet("contactos")]
        public async Task<IActionResult> ObtenerContactos()
        {
            try
            {
                var contactos = await _departamentoService.ObtenerDirectorioAsync();
                return Ok(contactos);
            }
            catch(Exception)
            {
                return StatusCode(500, new { error = "Ocurrio un error interno al obtener el directorio de contactos" });
            }
        }
    }
}