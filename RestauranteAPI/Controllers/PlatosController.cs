using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Data;
using RestauranteAPI.Models;

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatosController : ControllerBase
    {
        private readonly PlatoData _platoData;

        public PlatosController(PlatoData platoData)
        {
            _platoData = platoData;
        }

        // GET: api/platos
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var lista = await _platoData.Listar();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "Error al obtener platos", detalle = ex.Message });
            }
        }
    }
}