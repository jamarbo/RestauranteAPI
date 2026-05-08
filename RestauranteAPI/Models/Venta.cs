using System.Collections.Generic;

namespace RestauranteAPI.Models
{
    public class Venta
    {
        public int id_venta { get; set; }
        public int id_usuario { get; set; }
        public decimal total { get; set; }
        public string metodo_pago { get; set; }
        public DateTime fecha_venta { get; set; }

        // Esta es la clave: una venta ahora tiene muchos detalles
        public List<DetalleVenta> Detalles { get; set; }
    }
}