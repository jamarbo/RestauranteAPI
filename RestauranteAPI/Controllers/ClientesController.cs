using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Data;
using RestauranteAPI.Models;

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteData _clienteData;

        public ClientesController(ClienteData clienteData)
        {
            _clienteData = clienteData;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var lista = await _clienteData.Listar();
            return Ok(lista);
        }

        [HttpPost]
        [Route("registrar")]
        public async Task<IActionResult> Registrar([FromBody] Cliente objeto)
        {
            bool respuesta = await _clienteData.Registrar(objeto);
            if (respuesta)
                return Ok(new { resultado = true, mensaje = "Cliente Creado con éxito" });
            else
                return BadRequest(new { resultado = false, mensaje = "No se pudo crear el cliente" });
        }

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Editar([FromBody] Cliente objeto)
        {
            bool respuesta = await _clienteData.Editar(objeto);
            if (respuesta)
                return Ok(new { resultado = true, mensaje = "Cliente modificado con éxito" });
            else
                return BadRequest(new { resultado = false, mensaje = "No se pudo modificar el cliente. Verifique el ID." });
        }
    }
}