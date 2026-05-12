using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Data;
using RestauranteAPI.Models;

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaData _categoriaData;

        // El constructor recibe la lógica de datos
        public CategoriasController(CategoriaData categoriaData)
        {
            _categoriaData = categoriaData;
        }

        // GET: api/categorias
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var lista = await _categoriaData.Listar();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { msg = "Error al obtener categorías", detalle = ex.Message });
            }
        }

        [HttpPost]
        [Route("registrar")]
        public async Task<IActionResult> Registrar([FromBody] Categoria objeto)
        {
            bool respuesta = await _categoriaData.Registrar(objeto);

            if (respuesta)
            {
                // Devolvemos el mensaje de éxito personalizado
                return Ok(new { resultado = true, mensaje = "Categoría Creada con éxito" });
            }
            else
            {
                return BadRequest(new { resultado = false, mensaje = "No se pudo crear la categoría" });
            }
        }
    }
}