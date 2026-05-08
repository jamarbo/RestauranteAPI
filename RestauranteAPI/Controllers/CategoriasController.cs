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
    }
}