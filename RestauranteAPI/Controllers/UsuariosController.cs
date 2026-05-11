using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Data;
using RestauranteAPI.Models;

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioData _usuarioData;

        public UsuariosController(UsuarioData usuarioData)
        {
            _usuarioData = usuarioData;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _usuarioData.Listar();
            return Ok(lista);
        }

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            var objeto = await _usuarioData.Obtener(id);
            if (objeto == null) return NotFound();
            return Ok(objeto);
        }

        // POST: api/usuarios/registrar
        [HttpPost]
        [Route("registrar")]
        public async Task<IActionResult> Registrar([FromBody] Usuario objeto)
        {
            bool respuesta = await _usuarioData.Registrar(objeto);
            return Ok(new { resultado = respuesta });
        }
    }
}