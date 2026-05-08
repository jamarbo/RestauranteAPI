using Microsoft.AspNetCore.Mvc;
using RestauranteAPI.Data;
using RestauranteAPI.Models;

namespace RestauranteAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly VentaData _ventaData;
        private readonly DetalleVentaData _detalleData; // Nueva dependencia para los detalles

        // Actualizamos el constructor para recibir ambos servicios
        public VentasController(VentaData ventaData, DetalleVentaData detalleData)
        {
            _ventaData = ventaData;
            _detalleData = detalleData;
        }

        // POST: api/Ventas/Finalizar
        [HttpPost]
        [Route("Finalizar")]
        public async Task<IActionResult> FinalizarVenta([FromBody] Venta objeto)
        {
            try
            {
                // 1. Primero registramos la cabecera de la venta (Ventas)
                int idVentaGenerada = await _ventaData.FinalizarVenta(objeto);

                if (idVentaGenerada > 0)
                {
                    // 2. Si la venta se creó, registramos cada uno de sus platos en DetalleVenta
                    if (objeto.Detalles != null && objeto.Detalles.Count > 0)
                    {
                        foreach (var detalle in objeto.Detalles)
                        {
                            detalle.id_venta = idVentaGenerada; // Vinculamos el detalle a la venta creada
                            await _detalleData.RegistrarDetalle(detalle);
                        }
                    }

                    return Ok(new
                    {
                        mensaje = "Venta y detalles registrados con éxito",
                        id_venta = idVentaGenerada
                    });
                }
                else
                {
                    return BadRequest(new { mensaje = "No se pudo registrar la venta en la base de datos" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al procesar la venta completa",
                    error = ex.Message
                });
            }
        }

        // GET: api/Ventas
        [HttpGet]
        public async Task<IActionResult> ListarVentas()
        {
            try
            {
                var lista = await _ventaData.Listar();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener historial", error = ex.Message });
            }
        }
    }
}