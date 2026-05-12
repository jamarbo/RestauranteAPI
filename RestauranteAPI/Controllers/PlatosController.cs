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
        [HttpDelete]
        [Route("eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _platoData.Eliminar(id);
            if (respuesta)
                return Ok(new { resultado = true, mensaje = "Plato eliminado con éxito" });
            else
                return BadRequest(new { resultado = false, mensaje = "No se pudo eliminar el plato. Verifique si el ID existe." });
        }
    }
}